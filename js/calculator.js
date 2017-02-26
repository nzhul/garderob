// Initialize tooltips
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip(); 
});

// Initialize

$(document).ready(function () {
	var allSliderContainers = $('.calculator-slider');

	for (var i = 0; i < allSliderContainers.length; i++) {
		var container = $(allSliderContainers[i]);
		var slider = container.find('.slider-range-min');
		var amountInput = container.find('input.amount');

		var min = slider.data('min');
		var max = slider.data('max');
		var defaultValue = slider.data('default');

		amountInput.val(defaultValue + "cm");

		$(slider[0]).slider({
			range: "min",
			value: defaultValue,
			animate: true,
			min: min,
			max: max,
			slide: function (event, ui) {
				$(event.target).parents('.calculator-slider').find('input.amount').val(ui.value + "cm");
			}
		});
	}
});


// Calculation
