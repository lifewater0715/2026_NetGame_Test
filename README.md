# 네트워크 게임 공부 태스트

## 참고 문헌
https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@2.13/manual/tutorials/get-started-with-ngo.html
-------
## RPC형식
* RPC -> 네트워크 통신간에 언어에 구속되지 않고 쉽게 통신하기 위한 수?단?
* 해당 함수가 실행될 위치를 정하는 구조
* RPC <-- 이친구 쿼터니언 값 직열화 못함;;

-------
## 플래이어 이동 로직
* 지금은 Network Transform 인스팩터로 동기화중
-------
## 탄환 생성 로직
### 발사
* 플래이어 탄환 발사 -> 서버인지 검사 -> 서버로 총구 위치정보 전송 RPC 사용 -> 서버에서 탄환 생성 -> 클라이언트로 탄환 위치정보 전송 -> 클라이언트에 탄환 드로우
### 명중 검사
* (서버에서) 탄환 생성 -> 

-------
## other

* IsOwner : 자신의 네트워크 객채인지? Bool 반환
* IsServer : 서버네트워크 객채인지? Bool 반환

-------

