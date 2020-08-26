# !/usr/bin/env python
#

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
