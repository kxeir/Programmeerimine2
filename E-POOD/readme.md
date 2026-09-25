# E-poe andmemudel

## 1. Kategooria (Category)
- `id`: long – Unikaalne ID
- `name`: String – Kategooria nimi (nt "Kiivrid")

## 2. Toode (Product)
- `id`: long – Unikaalne ID
- `categoryId`: long – Viide kategooriale (`Category.id`)
- `name`: String – Toote nimi
- `explanation`: String – Kirjeldus
- `fotoUrl`: String – Pildi link
- `price`: BigDecimal – Praegune hind kaupluses

## 3. Tellimus (Order)
- `id`: long – Unikaalne ID
- `clientName`: String – Kliendi nimi
- `clientEmail`: String – Kliendi e-posti aadress
- `createTime`: LocalDateTime – Tellimuse loomise aeg
- `status`: String – Olek (Uus, Makstud, Tarnitud)

## 4. Ostukorvi rida (OrderInfo)
- `id`: long – Unikaalne ID
- `orderId`: long – Viide tellimusele (`Order.id`)
- `productId`: long – Viide tootele (`Product.id`)
- `priceNow`: BigDecimal – Hind ostu hetkel
- `quantity`: int – Kogus

<img width="852" height="1102" alt="E-POOD drawio" src="https://github.com/user-attachments/assets/5b9af101-c31c-4ea4-9e87-eea3dd607363" />
