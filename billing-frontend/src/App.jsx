import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { useState } from "react";
import Login from "./pages/Login";
import Plans from "./pages/Plans";
import Invoices from "./pages/Invoices";
import MySubscriptions from "./pages/MySubscriptions";
import Navbar from "./components/Navbar";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(
    !!localStorage.getItem("token")
  );

  return (
    <BrowserRouter>
      {isLoggedIn && (
        <Navbar onLogout={() => setIsLoggedIn(false)} />
      )}

      <Routes>
        <Route
          path="/login"
          element={
            isLoggedIn ? (
              <Navigate to="/plans" replace />
            ) : (
              <Login onLogin={() => setIsLoggedIn(true)} />
            )
          }
        />

        <Route
          path="/plans"
          element={isLoggedIn ? <Plans /> : <Navigate to="/login" replace />}
        />

        <Route
          path="/subscriptions"
          element={isLoggedIn ? <MySubscriptions /> : <Navigate to="/login" replace />}
        />

        <Route
          path="/invoices"
          element={isLoggedIn ? <Invoices /> : <Navigate to="/login" replace />}
        />

        <Route path="*" element={<Navigate to="/login" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
