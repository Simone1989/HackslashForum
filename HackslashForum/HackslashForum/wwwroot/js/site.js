// Write your JavaScript code.

//events: https://api.meetup.com/find/upcoming_events?&sign=true&photo-host=public&lon=11.974560&page=20&text=tech&radius=50&lat=57.708870
//grupper: https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=20

window.addEventListener('load', function (event) {

	document.getElementById('getEvents').addEventListener('click', getEvents);

	function getEvents() {
		fetch('https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=20&key=785d217213c1c192a7e71c4df2734')
			.then(function (response) { return response.json(); })
			.then(data => {
				let output = '<h2>Grupper</h2>';
				data.forEach(function (group) {
					output += `
						<div>
							<h3>${group.name}</h3>
						</div>
					`;
					console.log(data);
				});

				document.getElementById('output').innerHTML = output;
			})
	}


});