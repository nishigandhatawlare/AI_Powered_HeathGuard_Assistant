// Video Conferencing Functionality
let localStream;
let remoteStream;
const patientVideo = document.getElementById("patient-video");
const doctorVideo = document.getElementById("doctor-video");

document.getElementById("start-call").onclick = async function () {
    // Request access to camera and microphone
    localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
    patientVideo.srcObject = localStream;
    // Assume remoteStream is provided via WebRTC or other means
    // Here, we're just mimicking it
    // remoteStream = ... (you would set this to the incoming stream)
    // doctorVideo.srcObject = remoteStream;
    console.log("Call started");
};

document.getElementById("end-call").onclick = function () {
    localStream.getTracks().forEach(track => track.stop());
    patientVideo.srcObject = null;
    doctorVideo.srcObject = null;
    console.log("Call ended");
};

document.getElementById("mute").onclick = function () {
    localStream.getAudioTracks()[0].enabled = false;
    console.log("Muted");
};

document.getElementById("unmute").onclick = function () {
    localStream.getAudioTracks()[0].enabled = true;
    console.log("Unmuted");
};

// Real-Time Chat Functionality
document.getElementById("send-chat").onclick = function () {
    const chatInput = document.getElementById("chat-input");
    const chatBox = document.getElementById("chat-box");

    if (chatInput.value) {
        const message = document.createElement("div");
        message.textContent = chatInput.value;
        chatBox.appendChild(message);
        chatBox.scrollTop = chatBox.scrollHeight; // Scroll to bottom
        chatInput.value = ""; // Clear input
    }
};

// File Sharing Functionality
document.getElementById("send-file").onclick = function () {
    const fileInput = document.getElementById("file-upload");
    if (fileInput.files.length > 0) {
        const file = fileInput.files[0];
        // You would typically upload this file to a server here
        console.log("File sent: " + file.name);
        fileInput.value = ""; // Clear input
    } else {
        alert("Please select a file to upload.");
    }
};

// Notes Functionality
document.getElementById("save-notes").onclick = function () {
    const notes = document.getElementById("doctor-notes").value;
    if (notes) {
        // Save the notes to the server or local storage
        console.log("Notes saved: " + notes);
        alert("Notes saved successfully!");
        document.getElementById("doctor-notes").value = ""; // Clear textarea
    } else {
        alert("Please enter some notes before saving.");
    }
};
// Save notes to local storage
document.getElementById("save-notes").onclick = function () {
    const notes = document.getElementById("doctor-notes").value;
    if (notes) {
        let savedNotes = JSON.parse(localStorage.getItem('savedNotes')) || [];
        savedNotes.push(notes);
        localStorage.setItem('savedNotes', JSON.stringify(savedNotes));
        displaySavedNotes();
        document.getElementById("doctor-notes").value = ""; // Clear textarea
        alert("Notes saved successfully!");
    } else {
        alert("Please enter some notes before saving.");
    }
};

// Display previously saved notes
function displaySavedNotes() {
    const notesList = document.getElementById("notes-list");
    notesList.innerHTML = ""; // Clear existing notes
    const savedNotes = JSON.parse(localStorage.getItem('savedNotes')) || [];
    savedNotes.forEach(note => {
        const noteDiv = document.createElement("div");
        noteDiv.textContent = note;
        notesList.appendChild(noteDiv);
    });
}

// Call displaySavedNotes on page load
window.onload = displaySavedNotes;
