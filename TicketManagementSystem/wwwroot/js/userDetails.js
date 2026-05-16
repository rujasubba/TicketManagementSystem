

$(document).ready(function () {
    $('#formErrors').hide();
    $('#formSuccess').hide();

    $('#userDropdown').on('change', function () {
        const selectedId = $(this).val();
        const user = usersData.find(u => u.id === selectedId);

        if (user) {
            $('#FirstName').val(user.firstName);
            $('#LastName').val(user.lastName);
            $('#Email').val(user.email);
            $('#PhoneNumber').val(user.phoneNumber ?? '');
            $('#Address').val(user.address ?? '');
            $('#Age').val('');
            $('#DepartmentId').val('');
        } else {
            $('#FirstName, #LastName, #Email, #PhoneNumber, #Address').val('');
            $('#Age').val('');
            $('#DepartmentId').val('');
        }
    });

    $('#userDetailsSubmit').on('click', function () {
        $('#formErrors').hide().html('');
        $('#formSuccess').hide().html('');

        const userId = $('#userDropdown').val();
        if (!userId) {
            $('#formErrors').html('Please select a user.').show();
            return;
        }

        const data = {
            userId: userId,
            firstName: $('#FirstName').val().trim(),
            lastName: $('#LastName').val().trim(),
            email: $('#Email').val().trim(),
            phoneNumber: $('#PhoneNumber').val().trim(),
            age: parseInt($('#Age').val()) || 0,
            address: $('#Address').val().trim(),
            departmentId: $('#DepartmentId').val() || null
        };

        console.log('Data:', data);

        $('#submitSpinner').show();
        $('#userDetailsSubmit').prop('disabled', true);

        $.ajax({
            url: '/UserDetails/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: {
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function () {
                $('#formSuccess').html('User details saved successfully.').show();
                $('#userDetailsForm')[0].reset();
                $('#FirstName, #LastName, #Email, #PhoneNumber, #Address').val('');
            },
            error: function (xhr) {
                if (xhr.status === 400) {
                    const errors = xhr.responseJSON;
                    let messages = '';
                    $.each(errors, function (key, val) {
                        $.each(val.errors, function (i, err) {
                            messages += err + '<br/>';
                        });
                    });
                    $('#formErrors').html(messages).show();
                } else if (xhr.status === 401) {
                    window.location.href = '/Identity/Account/Login';
                } else {
                    $('#formErrors').html('Something went wrong. Please try again.').show();
                }
            },
            complete: function () {
                $('#submitSpinner').hide();
                $('#userDetailsSubmit').prop('disabled', false);
            }
        });
    });

});