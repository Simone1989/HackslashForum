window.addEventListener('load', function (event) {


	//events: https://api.meetup.com/find/upcoming_events?&sign=true&photo-host=public&lon=11.974560&page=20&text=tech&radius=50&lat=57.708870
	//grupper: https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=10


	document.getElementById('getGroups');
	document.getElementById('getEvents');

	getGroups();

	// Fixa CORS
	function getGroups() {
		let proxy = 'https://cors-anywhere.herokuapp.com/';
		let api = 'https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=10&key=785d217213c1c192a7e71c4df2734';
		fetch(proxy + api, {
			method: 'GET',
			headers: {
				'Origin': 'localhost'
			}
		})
			.then(function (response) { return response.json(); })
			.then(data => {
				let outputGroups = '';
				data.forEach(function (group) {
					outputGroups += `
						<div>
							<br>
							<p span style="font-weight:bold">${group.name}</p>
							<p>Kommande meetup: ${group.next_event.name}</p>
							<a href="${group.link}" class="btn btn-primary">Länk</a>
							<br>
						</div>
					`;
					console.log(data);
				});
				document.getElementById('outputGroups').innerHTML = outputGroups;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				document.getElementById('outputGroups').innerHTML = JSON.stringify(error);
			});
	}




	// Window
});