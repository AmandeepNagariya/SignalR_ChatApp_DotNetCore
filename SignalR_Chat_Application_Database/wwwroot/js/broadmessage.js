const connection = new signalR.HubConnectionBuilder()
    .withUrl('/ChatHub')
    .build();

// Handle incoming messages from SignalR
connection.on('receiveMessage', function (message) {
    console.log('Received message:', message); // Debugging
    addMessageToChat(message);
});

// Start SignalR connection
connection.start()
    .catch(error => console.error('Error starting SignalR connection:', error));

// Add a message to the chat container
function addMessageToChat(message) {
    let chatContainer = document.getElementById('chatContainer');
    if (!chatContainer) return;

    let isCurrentUserMessage = message.userName === window.userName;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.textContent = message.userName;

    let text = document.createElement('p');
    text.textContent = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-left" : "time-right";
    when.textContent = new Date(message.when).toLocaleString('en-US', {
        month: 'numeric',
        day: 'numeric',
        year: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        hour12: true
    });

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chatContainer.appendChild(container);
}

// Function to send a message to the hub
function sendMessageToHub(message) {
    console.log('Sending message:', message); // Debugging

    // Convert the ISO string to Date object
    const whenDate = new Date(message.when);

    connection.invoke('sendMessage', message.userName, message.text, whenDate)
        .catch(err => console.error('Error sending message:', err));
}

// Bind the send message action to the submit button
document.getElementById('submitButton').addEventListener('click', () => {
    let text = document.getElementById('messageText').value.trim();
    if (text) {
        let when = new Date().toISOString(); // Use ISO string format
        let message = { userName: window.userName, text: text, when: when };

        sendMessageToHub(message);
        document.getElementById('messageText').value = ""; // Clear the input field
    }
});