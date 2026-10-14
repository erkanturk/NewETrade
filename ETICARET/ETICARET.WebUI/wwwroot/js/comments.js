// Ürün yorumları: ekle / düzenle / sil — sayfa yenilenmeden (AJAX).
// Sunucu tarafı: Controllers/CommentController.cs
//
// NOT: Eskiden jQuery $.ajax kullanılıyordu. Artık tarayıcının kendi fetch API'si + async/await.
// CSRF koruması: POST isteklerine Layout'taki <meta name="csrf-token"> değeri başlık olarak eklenir.

(() => {
    const box = document.getElementById('comments');
    if (!box) return;

    const productId = box.dataset.productId;
    const csrfToken = document.querySelector('meta[name="csrf-token"]').content;

    // POST isteği gönder, JSON cevabı döndür: { success: true, message: "..." }
    async function post(url, data) {
        const response = await fetch(url, {
            method: 'POST',
            headers: { 'RequestVerificationToken': csrfToken },
            body: new URLSearchParams(data)
        });
        return response.json();
    }

    // Yorum listesini sunucudan yeniden al (partial view → HTML)
    async function reload() {
        const response = await fetch(`/Comment/List?productId=${productId}`);
        box.innerHTML = await response.text();

        const count = box.querySelector('[data-comment-count]')?.dataset.commentCount ?? '0';
        document.querySelectorAll('.js-comment-count').forEach(el => el.textContent = count);
    }

    async function run(url, data) {
        try {
            const result = await post(url, data);
            showToast(result.message, result.success ? 'success' : 'danger');
            if (result.success) await reload();
        } catch {
            showToast('Sunucuya ulaşılamadı.', 'danger');
        }
    }

    // Event delegation: Liste yeniden yüklense bile olaylar "box" üzerinden yakalanır.
    box.addEventListener('submit', async event => {
        if (event.target.id !== 'comment-form') return;
        event.preventDefault();

        const text = event.target.elements.text.value.trim();
        if (!text) return showToast('Lütfen bir yorum yazın.', 'warning');

        await run('/Comment/Create', { productId, text });
    });

    box.addEventListener('click', async event => {
        const button = event.target.closest('[data-action]');
        if (!button) return;

        const item = button.closest('[data-comment-id]');
        const id = item.dataset.commentId;

        switch (button.dataset.action) {
            case 'edit':
                item.classList.add('is-editing');
                item.querySelector('textarea').focus();
                break;
            case 'cancel':
                item.classList.remove('is-editing');
                break;
            case 'save':
                await run('/Comment/Edit', { id, text: item.querySelector('textarea').value });
                break;
            case 'delete':
                if (confirm('Bu yorum silinsin mi?')) await run('/Comment/Delete', { id });
                break;
        }
    });
})();
