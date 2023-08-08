import { BrowserRouter } from "react-router-dom";
import "./App.css";
import Board from "./pages/BoardPage";
import AppRoutes from "./Routes";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <BrowserRouter>
          <AppRoutes></AppRoutes>
        </BrowserRouter>
      </header>
    </div>
  );
}

export default App;
