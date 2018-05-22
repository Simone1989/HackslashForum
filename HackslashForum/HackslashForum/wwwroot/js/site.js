window.addEventListener('load', function (event) {

	let outputGroupsDiv = document.getElementById('outputGroups');
	let userListDiv = document.getElementById('prolificUserList');
	let helpfulUserListDiv = document.getElementById('helpfulUserList');
	let adminListDiv = document.getElementById('adminList');
	let mostUpvotedPostParagraph = document.getElementById('mostUpvotedPost');

	getGroups();
	getProlificUsers();
	getHelpfulUsers();
	getAdminList();
	getMostUpvotedPost();

	function getMostUpvotedPost() {
		let mostUpvotedPostAPI = '/api/MostUpvotedPostAPI';
		fetch(mostUpvotedPostAPI)
			.then(response => { return response.json() })
			.then(data => {
				let mostUpvotedPost = '';
				mostUpvotedPost += `
					<p span style="font-weight: bold";>${data.title}</p>
					<p>Antal uppröstningar: ${data.upVotes}</p>
					`;
				mostUpvotedPostParagraph.innerHTML = mostUpvotedPost;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				mostUpvotedPostParagraph.innerHTML = JSON.stringify(error);
			});
	}

	function getAdminList() {
		let adminListAPI = '/api/AdminAPI';
		fetch(adminListAPI)
			.then(response => { return response.json() })
			.then(data => {
				let adminList = '';
				data.forEach(function (list) {
					adminList += `
					<div>
						<p span style="font-weight: bold";>${list.name}</p>
						<p>Mail: ${list.email}</p>
					</div>
					`
				});
				adminListDiv.innerHTML = adminList;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				adminList.innerHTML = JSON.stringify(error);
			});
	}


	function getHelpfulUsers() {
		let helpfulUsersAPI = '/api/HelpfulUsersAPI';
		fetch(helpfulUsersAPI)
			.then(response => { return response.json() })
			.then(data => {
				let helpfulUsersList = '';
				data.forEach(function (list) {
					helpfulUsersList += `
						<div>
							<p span style="font-weight: bold";>${list.userName}</p>
							<p>Mail: ${list.email}</p>
							
						</div>`;
				});
				helpfulUserListDiv.innerHTML = helpfulUsersList;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				helpfulUserListDiv.innerHTML = JSON.stringify(error);
			});
	}

	function getProlificUsers() {
		let userAPI = '/api/UsersAPI';
		fetch(userAPI)
			.then(response => { return response.json() })
			.then(data => {
				let userList = '';
				data.forEach(function (list) {
					userList += `
                        <div>
							<p span style="font-weight: bold";>${list.userName}</p>
							<p>Mail: ${list.email}</p>
						</div>`;
				});
				userListDiv.innerHTML = userList;
			})
			.catch(function (error) {
				console.log(JSON.stringify(error));
				userListDiv.innerHTML = JSON.stringify(error);
			});
	}

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