// Ödeme sayfası (Views/Order/Checkout.cshtml)
// - Ödeme yöntemine göre kart / havale bölümünü göster
// - Kart numarasını 4'lü gruplara ayır ve kart önizlemesini güncelle

document.addEventListener('DOMContentLoaded', () => {
    const cardSection = document.getElementById('card-section');
    const eftSection = document.getElementById('eft-section');
    const submitText = document.getElementById('submit-text');

    function updatePaymentType() {
        const isCard = document.querySelector('input[name="PaymentType"]:checked').value === 'CreditCard';
        cardSection.hidden = !isCard;
        eftSection.hidden = isCard;
        submitText.textContent = isCard ? 'Ödemeyi Tamamla' : 'Siparişi Onayla';
    }

    document.querySelectorAll('input[name="PaymentType"]').forEach(radio =>
        radio.addEventListener('change', updatePaymentType));
    updatePaymentType();

    // Kart önizlemesi
    const number = document.getElementById('CardNumber');
    const holder = document.getElementById('CardHolderName');
    const month = document.getElementById('ExpireMonth');
    const year = document.getElementById('ExpireYear');

    const previewNumber = document.getElementById('preview-number');
    const previewHolder = document.getElementById('preview-holder');
    const previewExpiry = document.getElementById('preview-expiry');

    function updateNumber() {
        const digits = number.value.replace(/\D/g, '').slice(0, 16);
        number.value = digits.replace(/(.{4})/g, '$1 ').trim();
        previewNumber.textContent = number.value || '•••• •••• •••• ••••';
    }

    function updateHolder() {
        previewHolder.textContent = holder.value.trim().toLocaleUpperCase('tr-TR') || 'AD SOYAD';
    }

    function updateExpiry() {
        previewExpiry.textContent = `${month.value || 'AA'}/${(year.value || 'YY').slice(-2)}`;
    }

    number.addEventListener('input', updateNumber);
    holder.addEventListener('input', updateHolder);
    month.addEventListener('change', updateExpiry);
    year.addEventListener('change', updateExpiry);

    // Form hatayla geri döndüyse önizleme dolu gelsin
    updateNumber();
    updateHolder();
    updateExpiry();
});
