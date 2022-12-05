## Java RAM

- Java Heap space is used by java runtime to allocate memory to Objects and JRE classes. 
- Java Stack memory is used for the execution of a thread. 

to use java command-line parameters to help control the RAM use of application:

- `-Xms`              set initial Java heap size when JVM starts
- `-Xmx`              set maximum Java heap size
- `-Xss`              set java thread stack size
- `-Xmn`              set the size of the Young Generation, rest of the space goes for Old Generation.

for example,
```
-Xms64m or -Xms64M   // 64 megabytes
-Xmx1g or -Xmx1G     // 1 gigabyte

java -Xmx64m -classpath ".:${THE_CLASSPATH}" ${PROGRAM_NAME}
```

![Java-Memory-Model](./res/java-memory-model.png)

## reference

- [Java (JVM) Memory Model - Memory Management in Java](https://www.digitalocean.com/community/tutorials/java-jvm-memory-model-memory-management-in-java)
