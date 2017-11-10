# LockServiceDemo





## 参考Demo ##



## 考虑问题 ##

1. 后台存储
2. 高并发
3. keep alive
4. 权限认证

解决方案：
1. redis，sql，mongo等支持事物处理的数据库均可
2. 受sql，redis支持并发的成都限制，可以考虑master-slave等手段来支持更高程度的并发
3. 由client端发起keep alive，



## 类似产品 ##

1. chubby lock
2. google script lock
 