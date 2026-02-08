import { useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

export default function Login({ onLogin }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const login = async () => {
    const res = await api.post("/Auth/login", { email, password });

    // SAVE TOKEN
    localStorage.setItem("token", res.data.token);

    // UPDATE APP STATE
    onLogin();

    // NAVIGATE
    navigate("/plans");
  };

  return (
    <div className="container mt-5 col-md-4">
      <h3 className="mb-3">Login</h3>

      <input
        className="form-control mb-2"
        placeholder="Email"
        onChange={e => setEmail(e.target.value)}
      />

      <input
        type="password"
        className="form-control mb-3"
        placeholder="Password"
        onChange={e => setPassword(e.target.value)}
      />

      <button className="btn btn-primary w-100" onClick={login}>
        Login
      </button>
    </div>
  );
}
