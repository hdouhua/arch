# 11周作业——安全架构与高可用架构

### 导致系统不可用的原因有哪些？保障系统稳定高可用的方案有哪些？请分别列举并简述。

待完成！

### 请用你熟悉的编程语言写一个用户密码验证函数，Boolean checkPW(String 用户ID, String 密码明文, String 密码密文)返回密码是否正确boolean值，密码加密算法使用你认为合适的加密算法。

常用的加密算法有对称加密、非对称加密和单向加密。

密码验证时一般是对用户输入的密码与存储在数据库中的密码进行对比，因为要储存下来，多使用单向加码，防止数据泄露时造成用户密码泄露。

常用的单向加密有 MD5、SHA 系列算法 和 HMAC，这些严格来讲不是加密算法，而是散列算法（hash）。

其中 HMAC 支持设置一个额外的密钥(通常被称为'salt'，加盐)来提高安全性。

以下使用`python`来实现一下密码验证的算法。

```python
import hmac
import hashlib

def is_null_or_empty(str1: str):
  if str1 is None or "".__eq__(str1.strip()):
    return True
  else:
    return False

def checkPW(user_id, passwd, cipher):
  if is_null_or_empty(user_id) or is_null_or_empty(passwd):
    return False

  hashed = hmac.new(user_id.strip().encode('utf-8'), passwd.strip().encode('utf-8'), hashlib.sha256)
  sig = hashed.hexdigest()
  # print(sig, hmac.compare_digest(sig, cipher))
  return hmac.compare_digest(sig, cipher)

if __name__ == "__main__":
  user_id = 'hdouhua'
  passwd = 'Hello, World!'
  cipher = '60143497aab5c3a6d50252fa31a9a13233ff70578c6e07143dadd4c9be4af697'

  print(f'{user_id} login in: {checkPW(user_id, passwd, cipher)}')

  incorrect_passwd = 'Hello,World!'
  print(f'{user_id} login in: {checkPW(user_id, incorrect_passwd, cipher)}')

  incorrect_passwd = '   '
  print(f'{user_id} login in: {checkPW(user_id, incorrect_passwd, cipher)}')
```

源码文件参考[check_passwd.py](./check_passwd.py) 或者 [ipython notebook](check_passwd.ipynb)
