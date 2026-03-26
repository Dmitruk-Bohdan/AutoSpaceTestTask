$(document).ready(function () {
    initEditButton();
    initSaveStoreButton();
});

// ================= OPEN MODAL =================
function initEditButton() {
    $(document).on('click', '.btn-edit', function () {
        const storeId = $(this).data('store-id');

        $.get(`/Store/${storeId}/edit`, function (html) {
            $('#editStoreModalContainer').html(html);

            const modalEl = document.getElementById('editStoreModal');
            const modal = new bootstrap.Modal(modalEl);
            modal.show();
        });
    });
}

// ================= SAVE =================
function initSaveStoreButton() {
    $(document).on('click', '#saveStoreBtn', function () {

        const form = $('#editStoreForm');

        const dto = {
            storeId: parseInt(form.find('[name="StoreId"]').val()),
            name: form.find('[name="Name"]').val(),
            address: form.find('[name="Address"]').val(),
            storeSchedulesDto: [],
            storeProductIds: []
        };

        const scheduleMap = {};

        form.find('[name^="ScheduleItems"]').each(function () {
            const name = $(this).attr('name');

            const match = name.match(/ScheduleItems\[(\d+)\]\.(Start|End|IsWorkingDay|DayOfWeek)/);
            if (!match) return;

            const index = parseInt(match[1]);
            const field = match[2];

            if (!scheduleMap[index]) {
                scheduleMap[index] = {};
            }

            if (field === 'DayOfWeek') {
                scheduleMap[index].dayOfWeek = parseInt($(this).val());
            }
            else if (field === 'Start') {
                scheduleMap[index].openTime = $(this).val();
            }
            else if (field === 'End') {
                scheduleMap[index].closeTime = $(this).val();
            }
            else if (field === 'IsWorkingDay') {
                scheduleMap[index].isDayOff = !$(this).is(':checked');
            }
        });

        dto.storeSchedulesDto = Object.values(scheduleMap);

        // ✅ PRODUCTS (FIXED)
        form.find('input[name="SelectedProductIds"]:checked').each(function () {
            dto.storeProductIds.push(parseInt($(this).val()));
        });

        console.log("DTO:", dto);

        $.ajax({
            url: '/store/update',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dto),

            success: function (res) {
                console.log("FULL RESPONSE:", res);
                console.log("STORE:", res.store);

                if (!res.success) {
                    alert(res.message || 'Error');
                    return;
                }

                // close modal
                const modalEl = document.getElementById('editStoreModal');
                const modal = bootstrap.Modal.getInstance(modalEl);
                if (modal) {
                    document.activeElement.blur();
                    modal.hide();
                }

                updateStoreRow(res.store);
            },

            error: function (xhr) {
                alert('Server error: ' + xhr.status);
            }
        });
    });
}

// ================= UPDATE TABLE =================
function updateStoreRow(store) {

    const row = $(`tr[data-store-id="${store.storeId}"]`);
    if (!row.length) return;

    row.find('.store-name').text(store.name);
    row.find('.store-address').text(store.address);
    row.find('.store-products-count').text(store.productsCount);

    if (store.storeSchedulesDto?.length) {

        const ordered = store.storeSchedulesDto.sort((a, b) =>
            ((a.dayOfWeek + 6) % 7) - ((b.dayOfWeek + 6) % 7)
        );

        let html = '';

        ordered.forEach(s => {
            const day = getDayShort(s.dayOfWeek);

            if (s.isDayOff) {
                html += `<div>${day}: off</div>`;
            } else {
                html += `<div>${day}: ${s.openTime} - ${s.closeTime}</div>`;
            }
        });

        row.find('.store-schedule').html(html);
    }
}

// ================= HELPERS =================
function getDayShort(day) {
    const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return days[day] || '';
}