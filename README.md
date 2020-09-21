# ParkingManagement

On my own time I created this small parking management app to deal with the parking problems of my current company.

![alt text](https://01ob3q.db.files.1drv.com/y4mK8ytYuhPLNwTnjcyaRnVC0m6nsUb9JKu1h3pDtOuOjGwc9xCKbjQ-j4QuObJJJ7huVcWWlICHXadkL2dliJz_PV5nVhZwM7JT87fCbSEiidypvovuw6u8Dgo29zC-MDkPdStAH4AxkDzJxM9SBIPZZSyHyCybnSJhnfIYGQKwyyU1i0CvNQYTh4_73X5z_S4ehNx81NUXMQxPI5lxkwFJg?width=617&height=217&cropmode=none)

All the parking management was being handled by email, which could get quite confusing and many emails could be sent in a normal day, so I decided to do something about it on my own time, it is not fancy but it cover all the scenarios:

-Create as many parking spots as you need

-Have parking spaces permanently assigned to one person (who has total control over it)

-Any User can take any free parking space on that same day (only one per person).

-Parking spaces can be booked for external clients by an administrator for the entire day or on a specific time frame (any user will be alerted of this time frame but can take the space out of it).

-An administrator user has total control to manage all parking spaces on any day.

-An normal user can manage parking spaces assigned on any day.

-It will add days automatically (users with assigned spots will be assigned by default), or an admin can add days in advance if something needs to be manually add for an specific day.

-Weekends are ignored.

-Azure Single Sign On is fully implemented (in the original version only employees can use it I updated this one to allow for login and register with some dafault users on the seeding)


![alt text](https://01oc3q.db.files.1drv.com/y4m-VgiJcrXPVVrqRgW-F7sxckFoEuaQGKJAaWXmIpaRMBcCKDywsYvkEc7S32FyA9k0xUD5LCAgM1Gtng1-WePQtDx2dT29nVFnkwfH4_hTQcpPsRglcFGfPKjpM5AxmppYfnyxz1ZcySkT6L5jHwQdudS_ownHQ0v5ZjRrQ1LfiTS9GQ0rdlhhiXwf5Masb_Okm99Xm2cFosys-lHpC8wng?width=534&height=525&cropmode=none)

![alt text](https://01ow3q.db.files.1drv.com/y4mvqyZyDODbBjdIE-8a1yME8vlUHP8v9xEEaiUNSbWcfT_Pze23z3SgTPzAOckyPeWAuR7ghunF3Kze_3p34QsqNsivwOBWAVD5iU8Hr-EFDcYu74XqyzHKA8wik_7o0K7x5Snt1ByntWQgSSChqHmUrWhekFdbKaiYoKSKytCDz1WYzgj12zRhnUFXtfzmADOw0medbjqruZLU562Y-OCQA?width=1283&height=547&cropmode=none)

![alt text](https://01ov3q.db.files.1drv.com/y4mYbAH-df8E6jviVRrwbmIDGpvu6ZMVg2Os8KyGanxiCGfSvMFz8b9iES7mQFM4CS2ou71rec25okjjxEWsSn4NI7t5sPvppzp9hj8ObCtWPjPUObbey1nzgw5QqCkhdC_5wv5P3zEBvvDEJxPSNhoGNwrSrKI37OTkqwfgz1aWzgHyd6qXY8vJbA-gaxS6y5hGppW9J_Wr649ImW2jwuTiQ?width=1280&height=297&cropmode=none)
