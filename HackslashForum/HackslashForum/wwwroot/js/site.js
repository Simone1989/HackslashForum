// Write your JavaScript code.

//events: https://api.meetup.com/find/upcoming_events?&sign=true&photo-host=public&lon=11.974560&page=20&text=tech&radius=50&lat=57.708870
//grupper: https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=10

window.addEventListener('load', function (event) {

	document.getElementById('getGroups').addEventListener('click', getGroups);
	document.getElementById('getEvents').addEventListener('click', getEvents);

	// Fixa CORS
	function getGroups() {
		fetch('https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&text=tech&radius=50&page=10&key=785d217213c1c192a7e71c4df2734')
			.then(function (response) { return response.json(); })
			.then(data => {
				let outputGroups = '<h2>Grupper</h2>';
				data.forEach(function (group) {
					outputGroups +=
						//<div>
						//	<h4>${group.name}</h4>
						//	${group.photo_link}
						//	<p>${group.next_event.name}</p>
						//	<p>${group.link}</p>
						//	<p>${group.city}</p>
						//</div>
						`
						<div class="card" style="width:400px">
						  <img class="card-img-top" src=${group.photo_link} alt="Card image" style="width:100px">
						  <div class="card-body">
							<br>
							<a href="${group.link}" class="btn btn-primary">${group.name}</a>
						  </div>
							<br>
						</div>
					`;
					console.log(data);
				});
				document.getElementById('outputGroups').innerHTML = outputGroups;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				document.getElementById('output').innerHTML = JSON.stringify(error);
			});
	}

	function getEvents() {
		fetch('https://api.meetup.com/find/upcoming_events?&sign=true&photo-host=public&lon=11.974560&page=20&text=tech&radius=50&lat=57.708870&key=785d217213c1c192a7e71c4df2734')
			.then(function (response) { return response.json(); })
			.then(data => {
				let outputEvents = '<h2>Meetups</h2>';
				data.forEach(function (event) {
					outputEvents +=
						`
						<div>
							<h4>${event.name}</h4>
							<p>När: ${event.local_date}, kl. ${event.local_time}
							<a href="${event.link}" class="btn btn-primary">Länk till meetup</a>
						</div>
					`;
					console.log(data);
				});
				document.getElementById('outputEvents').innerHTML = outputEvents;
			})
			//.catch(function (error) {
			//	console.log(JSON.stringify(error));
			//	document.getElementById('output').innerHTML = JSON.stringify(error);
			//});
	}




	// Window
});