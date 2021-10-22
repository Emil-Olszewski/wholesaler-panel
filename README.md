# wholesaler-panel
Wholesaler Panel is a simple app that simulates wholesales. App gets the data from storage, counts the final price for a specific customer, and shows it on the screen.

## Run

Clone the repository and restore NuGet packages.

## Usage

When you run the app you will see the following text:
```c
Użytkownicy:
[ID:285] Arthur Brown - rabat 23%
[ID:218] John Hall - rabat 14%
[ID:549] Michael Thatcher - rabat 17%
Wpisz ID użytkownika żeby się zalogować.
```

Every customer has a different discount on the entire assortment. Type CustomerId to login. Then you will be asked for a few parameters, like minimal available quantity or maximum base price.
```c
Wpisz maksymalną cenę bazową produktu:
```

Then you will see a list of products matching provided parameters. Here you can see a base price based on customer discount and particular product discount. You will also see a sale price for different quantities.
```c
[ID:795121] msn.com
Dostępna ilość: 438
Cena bazowa: 2804,60 PLN
Ceny dla ciebie:
        od 100 sztuk - 2095,04 PLN / szt
        od 250 sztuk - 1978,65 PLN / szt
        od 500 sztuk - 1745,86 PLN / szt
        od 1000 sztuk - 1396,69 PLN / szt
```

That's it!
