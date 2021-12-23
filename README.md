# SVTRoboticsAssesment
## Directions for running and testing the api

1. From Visual Studio launch on localhost using IIS Express.
2. Open Postman and create a new request.
3. Change the type of the request to POST.
4. In the url bar enter "http://localhost:5000/api/robots/closest"
5. Navigate to the Body tab.
6. Select "raw" and switch the type from text to JSON.
7. Enter json string as such.
{
    "loadId": 231,
    "x": 5,
    "y": 3
}
8. Click send.

## The next steps i would take in the project.

The next steps i would take in the project would be to calculate the distance from the pickup and drop off location, the way i would go about this would be to include this additional information in the initial post and expand the Payload model to accept the new Information. I would then run the same distance formula to get the new value. I would also want to calculate the battery usage for each trip to monitor efficiency. This could be accomplished by adding an additional field in the Robot model and calculating how much battery is used for the robot to move one unit of distance. I feel this information could be used to further select the best unit to select for future payload pickups.
