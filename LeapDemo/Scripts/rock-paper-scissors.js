var gestureFrameThreshold = 120;
var rockCount = 0;
var paperCount = 0;
var scissorsCount = 0;

//Connect to SigalR hub
var hub = $.connection.demoHub;
$.connection.hub.start().done(function () {
    $('#status').css('color', 'blue');
    $('#status').text('Connection Ready!!');
    var controller = new Leap.Controller({ enableGestures: true });

    //main function for leap motion device
    controller.loop(function (frame) {

        if (frameContainsPaperGesture(frame)) {
            triggerPaperGetsure();
        }
        if (frameContainsRockGesture(frame)) {
            triggerRockGetsure();
        }
        if (frameContainsScissorsGesture(frame)) {
            triggerScissorsGetsure();
        }

        var jsonString = JSON.stringify(frame.data);
        $('#test').text(jsonString);
    });
});

function triggerRockGetsure() {
    rockCount++;
    if (rockCount == gestureFrameThreshold) {
        hub.server.sendGesture('{"gesture" : "rock"}');
        rockCount = 0;
    }
}
function triggerPaperGetsure() {
    paperCount++;
    if (paperCount == gestureFrameThreshold) {
        hub.server.sendGesture('{"gesture" : "paper"}');
        paperCount = 0;
    }
}
function triggerScissorsGetsure() {
    scissorsCount++;
    if (scissorsCount == gestureFrameThreshold) {
        hub.server.sendGesture('{"gesture" : "scissors"}');
        scissorsCount = 0;
    }
}
function frameContainsPaperGesture(frame) {
    return frame.hands.length == 1 && frame.pointables.length == 5;
}

function frameContainsScissorsGesture(frame) {
    return frame.hands.length == 1 && frame.pointables.length == 2;
}

function frameContainsRockGesture(frame) {
    return frame.hands.length == 1 && frame.pointables.length == 0;
}