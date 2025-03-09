# Continent Explorer - Educational Game

## Overview

Continent Explorer is a WPF application designed to help users learn about different continents. It features a 3D globe, continent selection, information display, and a quiz to test the user's knowledge.

## Features

- **Continent Selection:** A list of continents is displayed in the main menu for easy selection.
- **3D Globe Visualization:** A 3D globe rotates to highlight the selected continent.
- **Continent Information:** A dedicated page displays a brief description, along with information about native animals and famous landmarks.
- **Knowledge Quiz:** Each continent includes a quiz to test the user's knowledge, providing explanations for each answer.

## Technologies Used

- **WPF (.NET):** Used for building the desktop application.
- **C#:** The primary programming language for the application logic.
- **XAML:** Used for designing the user interface.
- **Helix Toolkit:** Used for displaying the 3D globe.
- **Newtonsoft.Json:** Used for serializing and deserializing continent data from JSON files.
- **.NET 9.0:** The project is built using .NET 9.0.

## Project Structure

- **`ContinentExplorer` (Project Root):**
  - `Continent.cs`: Defines the data models (`Continent`, `Animal`, `Landmark`, `QuizQuestion`, `QuizResult`).
  - `GameWindow.cs`: Handles the main game logic, including globe rotation and continent selection.
  - `GameWindow.xaml`: Defines the layout for the main game window.
  - `ContinentDetailWindow.cs`: Handles the display of detailed information about a selected continent and the quiz functionality.
  - `ContinentDetailWindow.xaml`: Defines the layout for the continent detail window.
  - `MainWindow.cs`: Handles the main application window (start and settings).
  - `MainWindow.xaml`: Defines the main application window.
  - `Data/continents.json`: Contains the continent data, including descriptions, animal information, landmarks, and quiz questions.
  - `Resources/Images`: Contains images for continents, animals, and landmarks.
  - `Resources/Audio`: Contains audio files related to continents (e.g., ethnic music).

## Setup and Installation

1.  **Prerequisites:**

    - Visual Studio (2022 or later)
    - .NET 9.0 SDK

2.  **Clone the Repository:**

    ```bash
    git clone [repository URL]
    ```

3.  **Open the Project in Visual Studio:**

    - Navigate to the cloned directory.
    - Open the `ContinentExplorer.sln` file using Visual Studio.

4.  **Restore NuGet Packages:**

    - In Visual Studio, go to `Tools` \> `NuGet Package Manager` \> `Manage NuGet Packages for Solution...`.
    - Click on the `Restore` button to install the required packages (e.g., HelixToolkit, Newtonsoft.Json).

5.  **Set up `continents.json`:**

    - Ensure that the `continents.json` file is located in the `Data` folder within the project directory.
    - In Solution Explorer, select `continents.json`.
    - In the Properties window:
      - Set `Build Action` to `Content`.
      - Set `Copy to Output Directory` to `Copy if newer` or `Copy always`.

6.  **Build the Project:**

    - Go to `Build` \> `Build Solution` to compile the application.

7.  **Run the Application:**

    - Press `Ctrl+F5` or go to `Debug` \> `Start Without Debugging` to run the application.

## Usage

1.  **Main Menu:**

    - The main menu displays options for "START GAME" and "SETTINGS".
    - Click "START GAME" to begin exploring continents.

2.  **Game Window:**

    - A list of continents is displayed on the left panel.
    - Click on a continent's name to rotate the 3D globe and view information on the right panel.
    - Click "EXPLORE" to view details, images, and take a quiz about the selected continent.

3.  **Continent Detail Window:**

    - View a description of the continent.
    - Browse information about native animals and famous landmarks.
    - Take a quiz to test your knowledge.

## Contributing

Contributions are welcome! Feel free to fork this repository, make changes, and submit pull requests.
