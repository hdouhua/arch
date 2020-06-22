# 03周总结

## 设计模式

设计模式是一种可重复使用的解决方案。

### 设计模式的组成

- 模式的名称：言简意赅，表达设计思想。

- 待解决的问题：描述何时使用模式，及运用模式的环境。

- 解决方案：描述抽象方案的组成设计的元素（类和对象）、它们间的关系、职责以及合作。

- 结论：运用方案所带来的利弊。主要指它对系统弹性、可扩展性和可移植性的影响。

### 分类

#### 以功能分类

- 创建模式：对类实例化过程的抽象
- 结构模式：将类或对象结合在一起形成更大的结构
- 行为模式：在不同对象之间划分职责和算法的抽象化

#### 以实现方式分类

- 类模式：静态的，以继承的方式实现模式
- 对象模式：动态的，以组合的方式实现模式

### 常用模式

#### 简单工厂方法

![factory method pattern](http://www.plantuml.com/plantuml/png/RP1FIyD04CNlyodUJGNJ3o0bjDeg1Vz0sul7JNQIB6vsm-mK2UftTy5QbFIooxnvlpVCB98QqKFlOJcvDXI3KMK9HEhKC-vDhL56FB5sqH9TOBT_VLpdTxdTK-SiW_j1H_JSLoo9qfiBWn7J8tN6ndSNMZJ69pwXw-SL4TNUf8HT56N1V8xBpVL3z9R3Djehk93bnjICiiG7VMNWzb1Lde_0RFPP7c7QXCIYBeOpFVhvVxSey3Vb1hMmqJnZOZjbJfnqvnq7HL7Y7pJHYKXSsocQ67E9bWEBKRQeHgoMBxJWOFFXwHi0)

##### 优点

- Client 不依赖Sorter 的具体实现（如，BubbleSorter）
- Client遵守OCP——增加Sorter不影响client

##### 缺点

- Factory不遵守OCP——增加Sorter需要修改Factory

##### 改进方案

使用反射或配置文件，或者两者并用

#### 单例模式 Singleton

保证一个类只产生单一实例。

- 减少实例频繁创建和销毁产生的开销——性能需求
- 多个用户共享同一个实例，便于管理控制——功能需求

实现说明

- 私有化构造函数

- 多线程安全下的单一实例

- 尽量设计成无状态对象——只提供服务，不保存状态

#### 适配器模式 Adapter

系统需要使用现有的类，而这个类的接口与我们所需要的不同

- 类适配器
- 对象适配器

![adapter pattern](http://www.plantuml.com/plantuml/png/POwnZSCm40Jpgs8RO0CV21B-yq_Sm0bSKOH9YZYUOGXmyNQWpDbYWvcPXwlvenP4ZfnQwEfEQoAUlH1BrEuqaJr7WhNxYSgwqeQCUsvTJIl6hl5uvfQmQHaKaT-IsHnXmaqjqUK28OJNztTfY1_ejIOHiPnsntOSJaDYewapOZtnj_pBp1O_uMy0)

应用：

- JDBC driver

- JDBC-ODBC bridget

#### 模版方法 Template Method

扩展功能的最基本模式之一

一种*类*的“行为模式”

##### 通过“继承”来实现扩展

- 基类负责算法的骨架

- 子类负责算法的具体实现

- 基于“继承”的模版方法比“组合”更容易实现

![template method](http://www.plantuml.com/plantuml/png/bP51JiGm34NtFeMN8DG5701DALl4XWkuZQUDI9pADaAZOUvEWSwY425DTl7_F-yhtfcJ-bjE6DZYACrCxgm2uD4JumldEP4pFh5F9G1CCJ0kGpzWvjop6jXhb9cKCjf4eRn76N5FoHW0XaHcsFRcIeDttxSZu79ky4Zh-8HbPYMFHMwlhlzHxsgghnL_6YwEqndR5HcKLsnxvbPWMq8z_t19_XUvQJxkjvy0sxTh3kXIP75i_WYWSPmSzwKuAuuiNt0Es1_miNs67KjODle9)

##### 何时使用

重构系统时
- 将一个大方法打破，变成多个可扩展的步骤
- 将if/else或switch语句换成多态性

##### 可能产生的问题

- 将抽象算法和具体步骤耦合了，无法独立演化
- 造成类的数量很多、类的层级很深

#### 策略模式 Strategy

扩展功能的另一种最基本模式

一种*对象*的“行为模式”

##### 通过“组合”来实现扩展

![strategy pattern](http://www.plantuml.com/plantuml/png/XOyzJWGn38Lxd-AL89Ghh7OP6YeAJk1DF9c8_8mShs04t1q1oY9DG-dtVRRrZjrOxL8oWlGKgrlPJBGIMR8iU3PbaxWHBIa8fAoseKWvYtRgBwckh5pG5mxjDTM8cNCN8lAPUyVKgicRuPq0u0x-Ttwi9ZgpbvjR-XpkU7MLESUcoPQoqyVHvguxFw5NyJQI4vpxEFm9Utx_3vzsdAQSf1hSOPGY8arnCSojcIYYLFcM0yuR7avFT9GQBYL_0G00)

##### 何时使用

- 系统需要在多种算法中选一时使用
- 重构系统
  - 讲条件语句转换成对于策略的多态性调用

##### 可能存在的问题

策略模式仅仅封装了“算法的具体实现”，方便添加和替换算法。但何时使用何种算法，由客户端来决定。

##### 对比模版方法

- 将使用策略的人和策略的具体实现分离
- 策略的对象可以自由组合

#### 组合模式 Composite

一种*对象*的“结构模式”

![composite pattern](http://www.plantuml.com/plantuml/png/RL3TQlCm3BtNK-YovVlI1oWbT7Gm1eD1jps0coYfgMq5Is4hxTvzJjZcaDLtEZe_nzgAcgYzTw1M7U6EVSTYbG2EjYOat-aPFVa3HG4vsT2PQ3pAqTJyRaEBZN6IsD2PwcHQme-tO1Kl09XUvOm8lf8eRZQuUFp_mAwciETwYJOenSPKX6MP-FZz_c7imw1OB4ViSsRqqbiblCpXdP4RwBuFWU8DdAHtEk3Z8h0IAdV8pIHZdQ-HMGTMPP-8ENMDJsGQW6Gs9YHPg29LcdiDkzzfjRgMenfo5XV579lnqbXEkiFaUqdwOxuUhYVmfq5HN3C98DhsgDWmQvAtb4oCKes7CzxVFSEMGjrxzmK0)

##### 应用

- 文件系统

- GUI 控件

#### 装饰器模式 Decorator

一种*对象*的“结构模式”

在不改变对客户端接口的前提下，扩展现有对象的功能

装饰器模式和适配器模式都被笼统地称为“包装器”（wrapper），区别是适配器会转换成另一种接口，而装饰器是保持接口不变。

![decorator pattern](http://www.plantuml.com/plantuml/png/ZOxTIWGn34RlynIvLAIl8Bkwzpv3jitC5Dkaf1baKD_T7zI2ex2zRCvtE8VkPBxM0fwzC9uugf6h0ImvCN9jlc7bUcRhq1-pD3Ags1TA-fHbnebAPu1Og7UyrzfKM1oV0T_V3MfZ8yygLzVmEZ29nj4lmqVK5nOmVW__dktz8RpSZZeOndyF6iBu7yeVuFWv7Mrxukut0PRdnN5ITJQSMJwYEU5vmzFn0GuiQQtb3G00)

##### 装饰器、模板方法和策略模式的比较

装饰器保持对象的功能不变，而扩展外围的功能

模板方法和策略模式是保持算法的框架不变，而扩展其内部实现

##### 装饰器和继承的比较

用来扩展对象的功能

装饰器是动态的，而继承是静态的

装饰器可以任意组合，同时增加了复杂性

## JUnit 框架分析

#### JUnit框架的约定

遵循框架的约定来创建和测试测试用例

#### 排序实例说明

TLTR.


根据老师的实例分析去学习了xUnit 及 [xUnit.net](https://xunit.net/)

### 备注

所有的图片使用[PlantUML](http://www.plantuml.com/)生成，代码请看[res](./res)

