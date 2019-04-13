(function() {
	var now = moment();
	var expdate = moment($('#expiration_date_value').text(), 'DD-MM-YYYY').subtract(3, 'months');
	
	if (!now.isBefore(expdate)) {
		$('.expiration-date').addClass('text-danger font-weight-bold');
	}
})();
