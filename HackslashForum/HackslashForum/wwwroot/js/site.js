window.addEventListener('load', function (event) {

	let outputGroupsDiv = document.getElementById('outputGroups');
	//let userListDiv = document.getElementById('userList');
	//let btn = document.getElementById('getUserList');

	getGroups();
	//getProlificUsers();

	/*btn.addEventListener('click', getProlificUsers);
	function getProlificUsers() {
		let userAPI = '/api/GetApplicationUser';
		fetch(userAPI)
			.then(response => { return response.json() })
			.then(data => {
				let userList = '';
				data.forEach(function (list) {
					userList += `
						<div>${list.UserName}</div>`;
				});
				userListDiv.innerHTML = userList;
			});
	}*/

	function getGroups() {
		let proxy = 'https://cors-anywhere.herokuapp.com/';
		let api = 'https://api.meetup.com/find/groups?&sign=true&photo-host=public&country=se&upcoming_events=true&location=göteborg&radius=50&page=10&key=785d217213c1c192a7e71c4df2734';
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
				outputGroupsDiv.innerHTML = outputGroups;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				outputGroupsDiv.innerHTML = JSON.stringify(error);
			});
	}




	// Window
});