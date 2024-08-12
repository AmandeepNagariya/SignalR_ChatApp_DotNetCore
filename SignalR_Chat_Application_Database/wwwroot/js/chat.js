// Initialize SignalR connection
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/ChatHub')
    .build();

// Handle incoming messages from SignalR
connection.on('receiveMessage', function (message) {
    addMessageToChat(message);
});

// Start SignalR connection
connection.start()
    .catch(error => console.error('Error starting SignalR connection:', error));

// Add a message to the chat container
function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

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
    chat.appendChild(container);
}

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message.userName, message.text, message.when)
        .catch(err => console.error('Error sending message:', err));
}

// Example of how to send a message (e.g., on button click)
document.getElementById('submitButton').addEventListener('click', () => {
    let text = textInput.value.trim();
    if (text) {
        let when = new Date().toISOString();
        let message = { userName: username, text: text, when: when };
        sendMessageToHub(message);
        textInput.value = ""; // Clear the input field
    }
});
