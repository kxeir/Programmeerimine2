# E-poe andmemudel (KEIRI E-POOD)

## 1. Kasutaja (User)
- `Id`: Unikaalne ID
- `FirstName`: Eesnimi
- `LastName`: Perekonnanimi
- `Phone`: Telefoninumber
- `Email`: E-posti aadress
- `Address`: Aadress
- `UserName`: Kasutajanimi
- `Password`: Parool

## 2. Tootekategooria (ProductCategory)
- `Id`: Unikaalne ID
- `Name`: Kategooria nimi

## 3. Toode (Product)
- `Id`: Unikaalne ID
- `Name`: Toote nimi
- `Description`: Kirjeldus
- `Price`: Hind
- `ProductCategoryId`: Viide tootekategooriale (`ProductCategory.Id`)

## 4. Tellimus (Order)
- `Id`: Unikaalne ID
- `OrderDate`: Tellimuse kuupäev ja aeg
- `OrderState`: Tellimuse olek
- `UserId`: Viide kasutajale (`User.Id`)

## 5. Ostukorvi rida (OrderLine)
- `Id`: Unikaalne ID
- `Price`: Hind
- `Quantity`: Kogus
- `Total`: Kogusumma
- `ProductId`: Viide tootele (`Product.Id`)
- `OrderId`: Viide tellimusele (`Order.Id`)
