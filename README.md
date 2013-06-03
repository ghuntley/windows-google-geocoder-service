# Windows Google Geocoder Service



## Code Generation of Database Models

SourceDb.cs is automatically generated using SQLMetal. To regenerate it install the Microsoft Windows 7 or 8 SDK from Microsoft and run the following commands:

	# cd C:\PROGRA~2\Microsoft SDKs\Windows\v7A.0\Bin
	# sqlmetal /server:localhost /database:interactive /code:C:\tmp\SourceDb.cs /namespace:SourceDb /context:SourceDbDataContext
    