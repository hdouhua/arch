# 08周作业

以下两题，至少完成一道

## 有两个单向链表（链表长度分别为 m，n），这两个单向链表有可能在某个元素合并，如下图所示的这样，也可能不合并。现在给定两个链表的头指针，在不修改链表的情况下，如何快速地判断这两个链表是否合并？如果合并，找到合并的元素，也就是图中的 x 元素。请用（伪）代码描述算法，并给出时间复杂度和空间复杂度。

<div align=center>
<img src="./res/linkedlist-is-merged.png" alt="linkedlist-is-merged" width="70%;" />
</div>

题目分析：

链表合并，也就是从合并点开始到尾节点的元素全部相同。如果两链表合并了，这个合并点就是我们要寻找的节点元素x。

算法描述：

1. 指针 current1 指向 链表1，指针 current2 指向 链表2
2. 计算两个链表节点数差 n，如上图：链表节点差是 n=1，链表2 较长
3. 让长链表（链表2）先遍历 n 个节点位置
4. 开始遍历 链表1 和 链表2 并比较当前节点，
   - 若值相等，继续 步骤4
   - 若值不想等，重置x=nil
   - 若 current1 或 current2 任一为空，也就是遍历完了链表
     - x不为空，x就是链表合并点
     - 若x为空，说明链表不合并

伪代码

```c
current1 <- link1
current2 <- link2
// merged node
x = nil

// traverse the longer linkedlist by diff length
length1 = length of link1
length2 = length of link2
if length1 > length2 then
  diff = length1 - length2
  while current1 != nil && diff != 0 do
    current1 <- current1.next
    diff--
  end
else
  diff = lenght2 - length1
  while current2 != nil && diff != 0 do
    current2 <- current2.next
    diff--
  end
end

// traverse 2 linked list
while current1 != nil && current2 != nil do
  if current1.value = current2.value then
    if x = nil then 
      x = current1
    end
  else
    x = nil
  end
  current1 <- current1.next
  current2 <- current2.next
end

// assert
if current1 = nil && current2 = nil && x != nil then
  the merged node is x
else
  no merged node
end
```

算法的时间复杂度O(max(m, n))，也就是O(n)。

算法的空间复杂度，没有增加额外的存储空间，也就是O(1)。

## 请画出 DataNode 服务器节点宕机的时候，HDFS 的处理过程时序图。

待有空时补上。
