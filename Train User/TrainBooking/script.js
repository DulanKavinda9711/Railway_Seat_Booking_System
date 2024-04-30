document.addEventListener("DOMContentLoaded", function () {
    const searchBtn = document.getElementById("searchBtn");

    
    console.log(searchBtn); 

    searchBtn.addEventListener("click", function () {
        
        console.log("Search button clicked");

        
        const startLocationInput = document.getElementById("startLocation").value.trim();
        const endLocationInput = document.getElementById("endLocation").value.trim();
        const dateInput = document.getElementById("date").value;

      
        console.log("Start Location:", startLocationInput);
        console.log("End Location:", endLocationInput);
        console.log("Date:", dateInput);

        fetchTrains(startLocationInput, endLocationInput, dateInput);
    });

    function fetchTrains(startLocation, endLocation, date) {
        const formattedDate = formatDate(date);
        console.log("Formatted Date:", formattedDate); 

        const url = `https://localhost:7132/api/Train/search?startLocation=${startLocation}&endLocation=${endLocation}&date=${formattedDate}`;

        console.log("Fetching trains from:", url); 

        fetch(url)
            .then(response => {
                console.log("Response status:", response.status); 
                return response.json();
            })
            .then(trains => {
                console.log("Received trains:", trains); 
                displayTrains(trains);
            })
            .catch(error => console.error("Error fetching trains:", error));
    }

    function formatDate(inputDate) {
        
        const [year, month, day] = inputDate.split('-');

        
        const formattedDate = `${month}-${day}-${year}`;

        return formattedDate;
    }

    function displayTrains(trains) {
        const trainList = document.getElementById("trainList");
        trainList.innerHTML = "";

        if (trains.length === 0) {
            trainList.innerHTML = "<p>No trains found</p>";
            return;
        }

        const table = document.createElement("table");
        table.innerHTML = `
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Time</th>
                <th>Date</th>
                <th>Start Location</th>
                <th>End Location</th>
                <th>Box</th>
                <th>Select</th> <!-- New column for select button -->
               
            </tr>
        `;

        trains.forEach(train => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${train.id}</td>
                <td>${train.name}</td>
                <td>${train.time}</td>
                <td>${train.date}</td>
                <td>${train.startLocation}</td>
                <td>${train.endLocation}</td>
                <td>${train.box}</td>
                <td><button class="selectBtn" data-train='${JSON.stringify(train)}'>Select</button></td> <!-- Add select button -->
                
            `;
            table.appendChild(row);
        });

        trainList.appendChild(table);



        const selectButtons = document.querySelectorAll('.selectBtn');
        selectButtons.forEach(button => {
            button.addEventListener('click', function () {
                const trainData = JSON.parse(this.dataset.train);
                const userName = prompt("Enter Username:");
                const userNIC = prompt("Enter NIC:");
                let seatNumbers;
                let seatNumberString;

                
                while (true) {
                    seatNumberString = prompt("Enter Seat Number (up to 5 seats, separated by commas):");

                  
                    seatNumbers = seatNumberString.split(',').map(num => num.trim());

                    
                    if (seatNumbers.length <= 5) {
                        break; 
                    }
                    
                    alert("You can only book a maximum of 5 seats per person.");
                }

                
                seatNumberString = seatNumbers.join(',');

               
                const boxNumber = prompt("Enter Box Number:");

                
                if (userName && userNIC && seatNumberString && boxNumber) {
                    const userData = {
                        userName,
                        userNIC,
                        seatNumber: seatNumberString,
                        boxNumber,
                        ...trainData
                    };
                    saveUserData(userData);
                } else {
                    console.log("User canceled input or didn't provide all required information.");
                }
            });
        });


    }

    function saveUserData(userData) {
        const userDataTable = document.getElementById("userDataTable");
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${userData.userName}</td>
            <td>${userData.userNIC}</td>
            <td>${userData.seatNumber}</td>
            <td>${userData.boxNumber}</td>
            <td>${userData.id}</td>
            <td>${userData.name}</td>
            <td>${userData.time}</td>
            <td>${userData.date}</td>
            <td>${userData.startLocation}</td>
            <td>${userData.endLocation}</td>
            <td>${userData.box}</td>
            <td><button class="bookingBtn" data-user='${JSON.stringify(userData)}'>Booking</button></td> <!-- Add booking button -->
        `;
        userDataTable.appendChild(row);

        const bookingButtons = document.querySelectorAll('.bookingBtn');
        bookingButtons.forEach(button => {
            button.addEventListener('click', function () {
                const userData = JSON.parse(this.dataset.user);
                bookTrain(userData);
            });
        });
    }

    function bookTrain(userData) {
        if (parseInt(userData.seatNumber) > 5) {
            alert('You can only book a maximum of 5 seats per person.');
            return;
        }
        const postData = {
            trainName: userData.name,
            startLocation: userData.startLocation,
            endLocation: userData.endLocation,
            date: userData.date,
            time: userData.time,
            box: userData.box,
            seatNumber: userData.seatNumber,
            nic: userData.userNIC,
            userName: userData.userName
        };

        console.log('Sending request with data:', postData);

        fetch('https://localhost:7132/api/Booking', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postData)
        })
            .then(response => {
                console.log('Received response:', response);
                if (!response.ok) {
                    throw new Error('Failed to book the train');
                }
             
                const contentType = response.headers.get('content-type');
                if (contentType && contentType.includes('application/json')) {
                    return response.json();
                } else {
                    alert('Booking successful!');
                    return null;
                    
                }
            })
            .then(responseData => {
                if (responseData) {
                    alert('Booking successful!');
                    console.log('Response from server:', responseData);
                } else {
                    console.log('Response from server is empty.');
                }
            })
            .catch(error => {
                console.error('Error booking the train:', error);
            });
    }


});

document.addEventListener('DOMContentLoaded', function () {
    var panel = document.getElementById('panel2');
    var numSeatsPerRow = 10; 
    var numRows = 2; 
    var totalSeats = numSeatsPerRow * numRows;

    for (var i = 1; i <= totalSeats; i++) {
        var seat = document.createElement('div');
        seat.className = 'seat';
        seat.id = 's' + i; 
        seat.style.top = (Math.floor((i - 1) / numSeatsPerRow) * 80 + 20) + 'px';
        seat.style.left = ((i - 1) % numSeatsPerRow * 70 + 20) + 'px'; 

        
        var seatNumber = document.createElement('span');
        seatNumber.className = 'seat-number';
        seatNumber.innerText = i; 
        seat.appendChild(seatNumber); 

        panel.appendChild(seat);
    }
});

