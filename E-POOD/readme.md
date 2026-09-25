# E-poe andmemudel
1. Kategooria (Category)
•	id: long – ID
•	name: String – Nimi (nt "Kiivrid")
2. Toode (Product)
•	id: long – ID
•	categoryId: long – Mis kategooriasse käib
•	name: String – Toote nimi
•	explanation: String – Kirjeldus
•	fotoUrl: String – Pildi link
•	price: BigDecimal – Praegune hind pühakojas
3. Tellimus (Order)
•	id: long – ID
•	clientName: String – Kliendi nimi
•	clientEmail: String – Mail
•	createTime: LocalDateTime – Aeg, mil laks tehti
•	status: String – Olek (Uus, Makstud, Tarnitud)
4. Ostukorvi rida (OrderInfo)
•	id: long – ID
•	orderId: long – Mis tellimusele kuulub
•	productId: long – Mis toodet osteti
•	priceNow: BigDecimal – Hind ostu hetkel (et kampaania või hinnatõus ajalugu sassi ei peaks)
•	quantity: int – Kogus

<img width="852" height="1102" alt="E-POOD drawio" src="https://github.com/user-attachments/assets/5b9af101-c31c-4ea4-9e87-eea3dd607363" />
