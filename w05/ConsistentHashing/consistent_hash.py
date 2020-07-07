from hashlib import md5
from struct import unpack_from
from bisect import bisect_left
import statistics

# total number of key-value
ITEMS = 100_0000
# total number of node
NODE_COUNT = 10
# virtual nodes
REPLICAS = 200


def _hash(value):
    k = md5(str(value).encode('utf-8')).digest()
    ha = unpack_from(">I", k)[0]
    return ha

def make_hash_ring(node_count, replicas):
    ring = []
    hash2node = {}
    for n in range(node_count):
        for v in range(replicas):
            h = _hash(str(n) + '#' + str(v))
            ring.append(h)
            hash2node[h] = n
    ring.sort()

    return (ring, hash2node)

# def add_node(circle, node, replicas):
#     ring = circle[0]
#     hash2node = circle[1]
#     for v in range(replicas):
#         h = _hash(str(node) + '#' + str(v))
#         ring.append(h)
#         hash2node[h] = node
#     ring.sort()

# def remove_node(circle, node, replicas):
#     ring = circle[0]
#     hash2node = circle[1]
#     for v in range(replicas):
#         h = _hash(str(node) + '#' + str(v))
#         ring.remove(h)
#         hash2node.pop(h)
#     ring.sort()

def get_node(circle, item):
    ring = circle[0]
    hash2node = circle[1]
    total_node = len(ring)
    h = _hash(str(item))
    print('hash:', h)
    if h not in ring:
        print('slot:', bisect_left(ring, h))
        h = ring[bisect_left(ring, h) % total_node]
    node = hash2node[h]
    print('node:', node)
    return node

def stats(circle, new_circle = None):
    changed = 0

    ring = circle[0]
    hash2node = circle[1]
    total_node = len(ring)

    doMoved = not(new_circle is None)
    if doMoved:
        new_ring = new_circle[0]
        new_hash2node = new_circle[1]
        new_total_node = len(new_ring)
        node_count = int(new_total_node/REPLICAS)
    else:
        node_count = int(total_node/REPLICAS)

    node_stat = [0 for i in range(node_count)]

    for item in range(ITEMS):
        h = _hash(str(item))
        n = bisect_left(ring, h) % total_node
        if doMoved:
            n2 = bisect_left(new_ring, h) % new_total_node
            node_stat[new_hash2node[new_ring[n2]]] += 1

            if hash2node[ring[n]] != new_hash2node[new_ring[n2]]:
                changed += 1
        else:
            node_stat[hash2node[ring[n]]] += 1

    avg_node = statistics.mean(node_stat)
    max_node = max(node_stat)
    min_node = min(node_stat)

    print('---------')
    print("Total: %d, Nodes: %d, Replicas: %d" % (ITEMS, node_count, REPLICAS))
    print("Mean: %d" % avg_node)
    print("Max: %d\t(%0.2f%%)" % (max_node, (max_node - avg_node) * 100.0 / avg_node))
    print("Min: %d\t(%0.2f%%)" % (min_node, (avg_node - min_node) * 100.0 / avg_node))
    print("Std: %d" % (statistics.stdev(node_stat)))
    print("Moved: %d\t(%0.2f%%)" % (changed, changed * 100.0 / ITEMS))


if __name__ == '__main__':
  circle = make_hash_ring(NODE_COUNT, REPLICAS)
  stats(circle)

  print('\nremove a node: ')
  circle0 = make_hash_ring(NODE_COUNT - 1, REPLICAS)
  stats(circle, circle0)

  print('\nadd a node: ')
  circle1 = make_hash_ring(NODE_COUNT + 1, REPLICAS)
  stats(circle, circle1)

  print('\n---------')
  get_node(circle, 'abcd')
  print('\n---------')
  get_node(circle0, 'abcd')
  print('\n---------')
  get_node(circle1, 'abcd')
