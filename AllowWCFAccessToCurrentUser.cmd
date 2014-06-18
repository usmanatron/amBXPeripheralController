REM Allows the current user to use the Urls required for aPC without administrator access.
netsh http add urlacl url=http://+:80/amBXPeripheralController user=%USERNAME%
netsh http add urlacl url=http://+:80/aPCTest user=%USERNAME%