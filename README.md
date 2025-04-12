👨‍💻 PANDUAN UNTUK SEMUA ANGGOTA TIM
✅ 1. Clone Project dari GitHub Langkah awal untuk semua anggota, hanya dilakukan sekali

git clone https://github.com/tani-go/tani-go.git

cd mbok-rondo-game

🔁 2. Ambil Update Terbaru dari master (Setiap Hari) Agar project lokal kamu selalu sesuai dengan pekerjaan terbaru dari tim lain:

git checkout master

git pull origin master

🌿 3. Pindah ke Branch Pribadi Pindah ke branch kamu sendiri untuk mengerjakan tugas:

git checkout nama-kamu

misal git checkout yudha

🎨 4. Kerjakan Fitur Kamu Lakukan perubahan di Unity seperti biasa. Setelah selesai (bisa harian atau tiap milestone kecil):

⬆️ 5. Simpan dan Push Pekerjaan ke Branch Sendiri

git add .

git commit -m "Deskripsi perubahan, contoh: Tambah animasi NPC wanita"

git push origin fitur-nama-kamu

misal git push origin yudha

🔄 6. Setelah Admin Merge, Ambil Update Terbaru Lagi dari master

git checkout master

git pull origin master

Kemudian gabungkan ke branch kamu sendiri agar up-to-date:

git checkout -nama-kamu

git merge master

🧑‍💼 PANDUAN UNTUK ADMIN (NABIL)
**✅ 1. Ambil Semua Update dari Semua Branch

git fetch --all

🔍 2. Cek dan Uji Setiap Branch Sebelum Merge

🔄 3. Merge ke Branch master

git checkout master

git pull origin master # pastikan master versi terbaru

Lalu merge satu per satu

git merge yazid

git merge hilmy

git merge yudha

git merge echo

git merge nabil

⬆️ 4. Push ke Remote master

git push origin master

📣 5. Beritahu Semua Anggota untuk Update Project

Anggota tinggal melakukan:

git checkout master

git pull origin master

git checkout fitur-nama

git merge master
