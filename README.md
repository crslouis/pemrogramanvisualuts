## Informasi Pribadi

| **Atribut**      | **Detail**               |
|------------------|--------------------------|
| **Nama**         | Carlos Louis Fernando    |
| **NIM**          | 312310458                |
| **Kelas**        | TI.23.A5                 |
| **Mata Kuliah**  | Pemrograman Visual       |

## Deskripsi

Halo! Saya Carlos Louis Fernando, mahasiswa di program studi Teknik Informatika, kelas TI.23.A5. Saya sedang mengikuti mata kuliah **Pemrograman Visual**, yang berfokus pada pengembangan aplikasi menggunakan prinsip-prinsip desain visual untuk menciptakan antarmuka pengguna yang menarik dan fungsional.

Sebagai bagian dari mata kuliah ini, saya mengembangkan beberapa proyek yang memanfaatkan konsep-konsep desain antarmuka visual, serta pengembangan aplikasi berbasis grafis dan multimedia.

## Tujuan dan Fokus Proyek

Dalam mata kuliah **Pemrograman Visual**, saya fokus pada:
- **Desain Antarmuka Pengguna (UI)**: Mempelajari bagaimana merancang antarmuka yang intuitif dan user-friendly.
- **Pemrograman Aplikasi Grafis**: Menggunakan berbagai framework dan alat untuk membuat aplikasi dengan elemen grafis interaktif.
- **Pengembangan Multimedia**: Menyusun elemen-elemen visual dan multimedia yang menyatu dalam aplikasi untuk meningkatkan pengalaman pengguna.

## Keterampilan

| **Keahlian**           | **Detail**                      |
|------------------------|---------------------------------|
| **Bahasa Pemrograman** | Python, JavaScript, HTML, CSS   |
| **Framework**          | React, Bootstrap, Tkinter       |
| **Alat Desain**        | balsamic                        |
| **Database**           | MongoDB, MySQL                  |

## Kontak

Jika kamu ingin berdiskusi atau memiliki pertanyaan terkait proyek atau mata kuliah ini, jangan ragu untuk menghubungi saya melalui:
- **Email**: [carloslouis9999@gmail.com]

---

# MarshLaundryDb

MarshLaundryDb adalah platform layanan laundry berbasis web yang menyediakan berbagai fitur untuk memudahkan pengelolaan laundry, baik bagi pelanggan maupun pengelola. Dengan sistem yang terintegrasi dan mudah digunakan, MarshLaundryDb membantu mempermudah proses pemesanan, pengelolaan laundry, dan pembayaran secara efektif.

## 1. Setting Web Service dan Configuration Web

Untuk memulai penggunaan MarshLaundryDb, pastikan kamu telah melakukan konfigurasi web service dan pengaturan aplikasi dengan benar. Berikut adalah langkah-langkah konfigurasi yang perlu dilakukan:

### 1.1 **Install Dependencies (Untuk Backend dan Frontend)**

- **Backend (ASP.NET)**:
   - Pastikan kamu sudah menginstall **.NET SDK** dan **Runtime** pada mesin kamu.
   - Gunakan perintah berikut untuk menginstall dependensi di sisi backend:
     ```bash
     dotnet restore
     ```
   - Untuk menjalankan server backend, gunakan perintah berikut:
     ```bash
     dotnet run
     ```
   - Pastikan bahwa server ASP.NET berjalan pada `http://localhost:5258` (atau sesuai dengan pengaturan port yang telah dikonfigurasi).

- **Frontend (Bootstrap)**:
   - Proyek ini menggunakan **Bootstrap** untuk desain responsif dan antarmuka pengguna yang ramah.
   - Untuk menjalankan frontend secara lokal, cukup buka file `index.html` di browser atau gunakan server lokal jika ingin lebih interaktif.
   - Untuk menginstall dependensi frontend jika diperlukan:
     ```bash
     npm install
     ```

### 1.2 **Konfigurasi Database dengan MongoDB Compass**

- **Instalasi MongoDB Compass**:
   - Pastikan **MongoDB Compass** sudah terpasang di mesin kamu untuk memanage database.
   - Jika belum, kamu bisa mengunduhnya [di sini](https://www.mongodb.com/products/compass).

- **Pengaturan Database**:
   - Buka file konfigurasi di `config/database.js` dan pastikan untuk mengonfigurasi URI MongoDB kamu.
   - Contoh konfigurasi:
     ```javascript
     const mongoose = require('mongoose');
     mongoose.connect('mongodb://localhost:27017/marshlaundrydb', { useNewUrlParser: true, useUnifiedTopology: true });
     ```

- **Jalankan MongoDB**:
   - Jika menggunakan MongoDB secara lokal, pastikan server MongoDB kamu berjalan dengan perintah:
     ```bash
     mongod
     ```
   - Jika menggunakan MongoDB Atlas, pastikan kamu mendapatkan connection string dan menggantinya di `database.js`.

### 1.3 **Menjalankan Server**

- Setelah konfigurasi di atas selesai, kamu dapat menjalankan server menggunakan perintah:
   ```bash
   dotnet run


2. Nama Project dan Penjelasan Penggunaannya

Nama Project: MarshLaundryDb

Penjelasan:

MarshLaundryDb adalah aplikasi web yang dirancang untuk membantu usaha laundry dalam mengelola layanan mereka secara lebih efisien. Platform ini memungkinkan pelanggan untuk membuat pemesanan laundry secara online, memantau status laundry mereka, dan melakukan pembayaran secara terintegrasi. Admin dapat mengelola pesanan, memperbarui status laundry, serta menghasilkan laporan transaksi dengan mudah.

Alasan Menggunakan MarshLaundryDb:

Otomatisasi Proses: Mengurangi kesalahan manual dan meningkatkan efisiensi pengelolaan laundry.

Pengalaman Pengguna yang Lebih Baik: Dengan antarmuka yang responsif dan mudah digunakan, pelanggan dapat dengan mudah melakukan pemesanan dan memantau status laundry mereka.

Laporan dan Analitik: Memungkinkan pengelola untuk menghasilkan laporan tentang transaksi, status laundry, dan kinerja usaha secara real-time.

3. Bisnis Proses

Proses bisnis dalam MarshLaundryDb meliputi beberapa tahapan penting sebagai berikut:

3.1 Pelanggan Membuat Pesanan

Pelanggan melakukan pemesanan laundry melalui antarmuka pengguna yang mudah dipahami. Mereka memilih jenis layanan yang diinginkan, mengisi informasi pengiriman, dan memilih metode pembayaran.

3.2 Pengelolaan Pesanan oleh Admin

Admin menerima pesanan yang masuk dan mengupdate status laundry (misalnya: sedang dicuci, selesai, atau dalam proses pengeringan). Sistem secara otomatis mengingatkan admin jika ada pesanan yang perlu diperhatikan.

3.3 Pembayaran dan Penyelesaian Pesanan

Pelanggan dapat melakukan pembayaran menggunakan berbagai metode yang telah disediakan (misalnya: kartu kredit, transfer bank, pembayaran tunai).

Setelah pembayaran dikonfirmasi, status pesanan diubah menjadi "selesai", dan pelanggan diberitahu untuk mengambil laundry mereka.

3.4 Pengambilan Laundry

Pelanggan mengambil laundry mereka setelah status pesanan diperbarui menjadi selesai.

4. Model Data

Model data dalam MarshLaundryDb dirancang untuk menyimpan semua informasi yang diperlukan untuk pengelolaan pesanan dan transaksi. Berikut adalah beberapa model data yang digunakan:

4.1 User Model

Menyimpan data pengguna (pelanggan atau admin) termasuk nama, email, dan jenis akun.

Digunakan untuk mengidentifikasi dan mengelola hak akses pengguna di sistem.

4.2 Order Model

Menyimpan informasi terkait setiap pesanan, termasuk jenis layanan, harga, status pesanan, dan ID pengguna yang terkait.

Digunakan untuk melacak status laundry dari mulai pemesanan hingga pengambilan.

4.3 Payment Model

Menyimpan informasi terkait pembayaran untuk setiap pesanan, termasuk jumlah yang dibayar, metode pembayaran, dan status pembayaran.

Digunakan untuk memproses pembayaran dan memverifikasi transaksi.

5. Schema Tim dalam Mengelola Project

Proyek MarshLaundryDb dikelola oleh tim yang terdiri dari beberapa role utama yang bekerja bersama untuk memastikan pengembangan proyek berjalan lancar:

5.1 Frontend Developer

Bertanggung jawab untuk pengembangan antarmuka pengguna menggunakan Bootstrap. Mereka memastikan desain aplikasi responsif dan menarik di berbagai perangkat.

5.2 Backend Developer

Bertanggung jawab untuk pengembangan API menggunakan ASP.NET, memastikan server backend dapat menangani permintaan dengan efisien dan aman.

5.3 Database Administrator

Mengelola MongoDB Compass untuk memastikan data tersimpan dengan baik dan aman. Mereka bertanggung jawab untuk merancang skema database dan melakukan pemeliharaan data.

5.4 Project Manager

Mengelola keseluruhan proyek, timeline, dan koordinasi antar tim. Mereka memastikan proyek tetap pada jalur yang benar dan memenuhi tujuan yang diinginkan.

6. MOCK UP

Home
<img width="1902" height="950" alt="Screenshot 2025-11-04 130453" src="https://github.com/user-attachments/assets/1328b1bf-eb19-46f6-bdf5-bf382a7169db" />

home layanan
<img width="1894" height="948" alt="Screenshot 2025-11-04 130931" src="https://github.com/user-attachments/assets/8d0864bf-c742-47cd-a828-c17997a36814" />

home tentang
<img width="1895" height="660" alt="Screenshot 2025-11-04 131050" src="https://github.com/user-attachments/assets/615fd532-a032-4b6e-98eb-1133cc5ddafc" />

halaman register
<img width="1914" height="945" alt="Screenshot 2025-11-04 131303" src="https://github.com/user-attachments/assets/8a6cd2b9-97e3-4358-bfb8-f9410c33bdd8" />

halaman masuk
<img width="1918" height="821" alt="Screenshot 2025-11-04 131426" src="https://github.com/user-attachments/assets/dfa74417-53cc-4f6d-9d9b-9a58d2ef1585" />


**HALAMAN USER**

beranda user
<img width="1917" height="947" alt="Screenshot 2025-11-04 212929" src="https://github.com/user-attachments/assets/d8850708-794e-4959-b65c-f6e569154edb" />

halaman layanan laundry
<img width="1895" height="942" alt="Screenshot 2025-11-04 213357" src="https://github.com/user-attachments/assets/5cb2a2cf-a453-430f-9959-ce8db1d41e89" />

halaman keranjang
<img width="1917" height="822" alt="Screenshot 2025-11-04 213416" src="https://github.com/user-attachments/assets/d3550b6f-2335-434b-9922-445659d0e13d" />

halaman pesanan saya
<img width="1919" height="825" alt="Screenshot 2025-11-04 213734" src="https://github.com/user-attachments/assets/04316834-8008-43e9-8458-47e2f34d32d5" />

halaman profile  
<img width="1919" height="711" alt="Screenshot 2025-11-04 213428" src="https://github.com/user-attachments/assets/ad51d0c9-3f23-458c-91c1-42a60c2cbb53" />




**HALAMAN ADMIN**

halaman dashbord
<img width="1913" height="829" alt="Screenshot 2025-11-04 213958" src="https://github.com/user-attachments/assets/8fb77dc2-9d87-4b2a-a4e1-83fd9ca7e367" />

halaman layanan
<img width="1919" height="826" alt="Screenshot 2025-11-04 214010" src="https://github.com/user-attachments/assets/b6bef282-db59-470d-ba8e-e79174448baf" />

halaman pesanan
  <img width="1913" height="828" alt="Screenshot 2025-11-04 214020" src="https://github.com/user-attachments/assets/95c9f4a2-7beb-4a1e-9985-2946d365f84c" />

halaman kelola user
<img width="1917" height="826" alt="Screenshot 2025-11-04 214034" src="https://github.com/user-attachments/assets/609cf794-b37b-4ce2-bad5-3e658b181eba" />


