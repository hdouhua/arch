# 05周总结

## 分布式缓存架构

介于数据访问者和数据源之间的一种高速存储，当数据需要多次读取的时候，用于加快读取的速度。

缓存cache vs 缓冲buffer ？

#### 无处不在的缓存

- CPU 缓存
- 操作系统缓存
- 数据库缓存
- 编译缓存
- CDN缓存
- 代理与反向代理缓存
- 应用程序缓存
- 分布式对象缓存

#### 缓存的关键指标

##### 缓存命中率

查询一个缓存，十次查询有九次能够得到正确结果，那么它的命中率是90%

缓存是否有效，依赖于能多少次重用同一缓存响应业务请求

##### 影响缓存命中率的因素

- 缓存键集合大小

  定位缓存对象的唯一方式就是对缓存键进行精确匹配。唯一键越多，重用的机会越小，因此我们要尽量减少缓存键

- 缓存可使用内存空间

  因为缓存通常存储在内存中，缓存可使用内存空间直接决定了缓存对象的平均大小和缓存对象数量。能缓存的对象越多，缓存命中率就越高

- 缓存对象生存时间TTL

  缓存对象的生存时间由业务场景决定。对象缓存的时间越长，被重用的可能性就越高。


##### 通读缓存 read-through

通读缓存给客户端返回缓存资源，并在请求未命中缓存时获取实际数据

客户端连接的是通读缓存，而不是生成响应的原始服务器

代理缓存、反向代理缓存、CDN缓存都是通读缓存

##### 旁路缓存 cache-aside

对象缓存是一种旁路缓存，通常是独立的键值对存储

应用代码通常会询问对象缓存需要的对象是否存在，如果存在，使用其；如果不存在或已过期，应用会连接主数据源来组装对象，并起将其保存回对象缓存，以便将来使用。

##### 本地对象缓存

对象直接存储在应用程序内存中

对象存储在共享内存，同一台机器的多个进程可以访问它

缓存服务器作为独立的应用和应用程序部署在同一个服务器上

##### 本地/远程对象缓存构建分布式集群

##### 分布式对象缓存的一致性hash算法

衡量一致性hash算法的几个标准：

- 平衡性balance
- 单调性monotonicity
- 分散性spread
- 负载性load

##### 各种介质的数据访问延迟

<img src="./res/latency.jpg" alt="latency" style="zoom:40%;" />

##### 技术栈各层次的缓存

<img src="./res/cache-in-tier.jpg" alt="cache-in-tier" style="zoom:50%;" />

##### 缓存为什么能显著提高性能

- 缓存数据通常来自内存
- 缓存存储了数据的最终结果形态，不需要中间计算，减少了CPU资源消耗
- 缓存降低了数据库、磁盘、网络的负载压力

##### 缓存是系统性能优化的大杀器

- 技术简单
- 性能提升显著
- 应用场景多

##### 合理使用缓存

不合理的使用缓存非但不能提高系统的性能，还会成为系统的累赘，甚至风险。

- 频繁修改的数据

  缓存这种数据徒增系统的负担。一般来说读写比在2:1以上，缓存才有意义

- 没有热点的访问

  应用程序访问缓存数据没有热点，不遵循二八定律，大部分数据访问不是集中在小部分数据上，即大部分数据还没有被再次访问就被挤出缓存了。

- 数据不一致和脏读

  - 对缓存数据设置失效时间，在缓存失效前，应用需要忍受一定时间的数据不一致。

  - 数据更新时立即更新缓存，会带来更多的系统开销和事务一致性问题
  - 数据更新时通知缓存失效，删除该缓存数据

- 缓存雪崩

  缓存承担了大部分的数据访问压力，数据库已经习惯了有缓存的日子，当缓存服务崩溃时，数据库会因为完全不能承受如此巨大的压力而宕机，进而导致整个网站不可用。发生这种故障时，甚至不能简单重启缓存服务器和数据库来恢复网站访问。

- 缓存预热 warm up

  缓存中存放的都是热点数据，热点数据可能是通过LRU算法计算出来的，这个过程可能要花费较长的时间，这段时间系统性能和数据库负载都不好，最好的做法是缓存系统启动时就把热点数据加载好。尤其是一些元数据。

- 缓存穿透

  不恰当的业务、或者恶意攻击持续高并发的请求某个不存在的数据，因为缓存没有保存该数据，所有的请求都会落到数据库上，会对数据库造成很大的压力，甚至崩溃。一个简单的对策是将不存在的数据也缓存起来（比如值设为null），并设置一个较短的失效时间。


## 消息队列与异步架构

缓存提高系统的读能力

消息队列提升系统的写能力



## 负载均衡

### IP负载均衡

缺点：负载均衡的负载受制于出口带宽

### 数据链路层负载均衡





## 分布式数据库



MYSQL主从架构

主库上把DDL和DML记录到binlog，然后由专用进程同步binlog到从数据库relaylog

一般会关闭主数据库的DDL不同步到从数据库

数据库的主主复制：保证主服务器宕机时数据库的高可用
