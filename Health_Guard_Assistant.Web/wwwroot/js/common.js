// Inbox Badge Increment
document.addEventListener('newMessageReceived', function (event) {
    const inboxBadge = document.querySelector('.nav-item.active .badge');
    if (inboxBadge) {
        let currentCount = parseInt(inboxBadge.textContent, 10) || 0;
        inboxBadge.textContent = currentCount + 1;
    }
});

// Modal for setting appointment data
document.addEventListener('DOMContentLoaded', function () {
    const appointmentModal = document.getElementById('appointmentModal');
    if (appointmentModal) {
        appointmentModal.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;
            const providerId = button.getAttribute('data-provider-id');
            const providerName = button.getAttribute('data-provider-name');

            // Set modal fields only if they exist
            const modalProviderId = appointmentModal.querySelector('#modalProviderId');
            const modalProviderName = appointmentModal.querySelector('#modalProviderName');
            if (modalProviderId) modalProviderId.value = providerId;
            if (modalProviderName) modalProviderName.value = providerName;
        });
    }

    const updateModal = document.getElementById('updateAppointmentModal');
    const cancelModal = document.getElementById('cancelAppointmentModal');

    // Update Modal
    if (updateModal) {
        updateModal.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;
            const appointmentId = button.getAttribute('data-appointment-id');
            const providerId = button.getAttribute('data-provider-id');
            const appointmentDate = button.getAttribute('data-appointment-date');
            const status = button.getAttribute('data-status');
            const patientName = button.getAttribute('data-patient-name');


            updateModal.querySelector('#updateAppointmentId').value = appointmentId;
            updateModal.querySelector('#updateAppointmentDate').value = appointmentDate;
            updateModal.querySelector('#updateStatus').value = status;
            updateModal.querySelector('#patientName').value = patientName;
        });
    }

    // Cancel Modal
    if (cancelModal) {
        cancelModal.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;
            const appointmentId = button.getAttribute('data-appointment-id');
            cancelModal.querySelector('#cancelAppointmentId').value = appointmentId;
        });
    }
});

// Symptom Form AI Recommendation
document.getElementById('symptomForm')?.addEventListener('submit', function (event) {
    event.preventDefault();
    const symptom = document.getElementById('symptom').value;
    const duration = document.getElementById('duration').value;
    const severity = document.getElementById('severity').value;

    let recommendationText = "Based on your input, we recommend you ";

    if (severity === '3') {
        recommendationText += "see a doctor immediately.";
    } else if (duration.includes('week') || (duration.includes('days') && severity === '2')) {
        recommendationText += "monitor your symptoms and consider seeking medical advice if they persist.";
    } else {
        recommendationText += "take rest and stay hydrated.";
    }

    document.getElementById('recommendation').textContent = recommendationText;
    document.getElementById('bookConsultation').style.display = 'block';
});

// FullCalendar Initialization
document.addEventListener('DOMContentLoaded', function () {
    const calendarEl = document.getElementById('calendar');
    if (calendarEl) {
        const calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            events: [
                // Add your events here
            ]
        });
        calendar.render();
    }
});

// Video Call Handling
document.getElementById('start-call')?.addEventListener('click', function () {
    alert('Starting video call...');
});

document.getElementById('end-call')?.addEventListener('click', function () {
    alert('Ending video call...');
});

document.getElementById('mute')?.addEventListener('click', function () {
    alert('Muted');
});

document.getElementById('unmute')?.addEventListener('click', function () {
    alert('Unmuted');
});

// Chat Handling
document.getElementById('send-chat')?.addEventListener('click', function () {
    const message = document.getElementById('chat-input').value.trim(); // Trim whitespace
    const chatBox = document.getElementById('chat-box');
    if (message === '') return;

    const newMessage = document.createElement('p');
    newMessage.textContent = "You: " + message;
    chatBox.appendChild(newMessage);
    document.getElementById('chat-input').value = ''; // Clear input after sending
    chatBox.scrollTop = chatBox.scrollHeight; // Scroll to the bottom
});

// Initialize System Stats Chart
document.addEventListener('DOMContentLoaded', function () {
    const ctx = document.getElementById('systemStats')?.getContext('2d');
    if (ctx) {
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June'],
                datasets: [{
                    label: 'System Usage',
                    data: [30, 45, 60, 70, 55, 85],
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }
});

// Initialize Health Metrics Chart
const healthMetricsCtx = document.getElementById('healthMetricsChart')?.getContext('2d');
if (healthMetricsCtx) {
    const healthMetricsChart = new Chart(healthMetricsCtx, {
        type: 'line',
        data: {
            labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'], // Example labels
            datasets: [{
                label: 'Heart Rate (bpm)',
                data: [72, 70, 75, 74], // Example data
                borderColor: 'rgba(78, 115, 223, 1)',
                backgroundColor: 'rgba(78, 115, 223, 0.2)',
                fill: true,
            },
            {
                label: 'Blood Pressure (mmHg)',
                data: [120, 118, 122, 119], // Example data
                borderColor: 'rgba(28, 200, 138, 1)',
                backgroundColor: 'rgba(28, 200, 138, 0.2)',
                fill: true,
            }]
        },
        options: {
            scales: {
                x: {
                    beginAtZero: true
                },
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

// Example of real-time data update using AJAX
function updateHealthMetrics() {
    // Use AJAX to fetch new data from the server and update the chart
    $.ajax({
        url: '/path-to-your-api-endpoint', // Update with your API endpoint
        method: 'GET',
        success: function (data) {
            // Assuming data structure matches the expected format
            healthMetricsChart.data.datasets[0].data = data.heartRate || []; // Safely access data
            healthMetricsChart.data.datasets[1].data = data.bloodPressure || []; // Safely access data
            healthMetricsChart.update();
        },
        error: function (error) {
            console.error("Error fetching health metrics:", error); // Log any errors
        }
    });
}

// Example: Update chart every 5 minutes
setInterval(updateHealthMetrics, 300000);
