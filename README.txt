# Municipal Services Application

## Description
A Windows Forms application for citizens to report municipal issues like potholes, water leaks, etc. This is the initial implementation for Part 1 of the project.

## How to Compile and Run
1.  Ensure you have **Visual Studio** installed with .NET Framework support.
2.  Open the solution file `MunicipalServicesApp.sln`.
3.  To compile, press **F6** or go to Build -> Build Solution.
4.  To run the application, press **F5** or go to Debug -> Start Debugging.

## How to Use the Software
1.  **Starting Up:** The application opens to the Main Menu.
2.  **Reporting an Issue:**
    - Click the "Report Issues" button.
    - Fill in the location of the problem (e.g., "Corner of Main and 1st").
    - Select a category from the dropdown list (e.g., "Potholes").
    - Provide a detailed description of the issue.
    - (Optional) Click "Attach Image/Document" to add a photo or file related to the issue.
    - A progress bar will fill up as you complete the form.
    - Click "Submit Report" to send your report. You will receive a confirmation message with a reference number.
3.  **Navigation:** Click "Back to Main Menu" to return to the main screen.

## Notes
- The "Local Events and Announcements" and "Service Request Status" features are disabled in this version as per the requirements.
- All data is stored in memory during runtime and will be lost when the application is closed. A database will be implemented in a future version.