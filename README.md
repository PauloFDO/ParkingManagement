# ParkingManagement

I created this small parking management app to deal with the parking problems in my current company:

-Some parking spaces are assigned to specific people
-Some and some are free for everyone
-Public parking spaces could be assigned to clients.
-Private parking spaces could be free on specific days

All of that was being handle with emails, which could get quite confusing and many emails could be send in anormal day, so I decided to do something about it on my own time, with this app you can:

-Have parking spaces permanently assigned to one person (who has total control to free it)
-Any User can take any free parking space on that same day (only one per person).
-Parking spaces can be booked for external clients by an administrator, on a specific time frame (any user will be alerted of this time frame but can take the space out of it).
-An administrator user has total control to manage all parking spaces days in advance.
-An normal user can manage parking spaces assigned to them days in advance.
-It will add days automatically (assigned spots will be assigned by default), or admin can add days in advance if something needs to be manually add for an specific day.
-Weekends are not add.
-Azure Single Sign On is fully implemented (in the original version only employees can use it)
