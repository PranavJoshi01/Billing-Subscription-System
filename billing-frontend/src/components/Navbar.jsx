import { Link, useNavigate } from "react-router-dom";

export default function Navbar({ onLogout }) {
  const navigate = useNavigate();

  const logout = () => {
    console.log("Logout clicked"); // 🔴 DEBUG LINE

    localStorage.removeItem("token");

    if (onLogout) {
      onLogout();   // update App state
    }

    navigate("/login", { replace: true });
  };

  return (
    <nav className="navbar navbar-dark bg-dark px-3">
      <span className="navbar-brand">SaaS Billing System</span>

      <div>
        <Link className="btn btn-link text-light" to="/plans">Plans</Link>
        <Link className="btn btn-link text-light" to="/subscriptions">My Subscriptions</Link>
        <Link className="btn btn-link text-light" to="/invoices">Invoices</Link>

        <button
          className="btn btn-outline-light btn-sm ms-3"
          onClick={logout}
        >
          Logout
        </button>
      </div>
    </nav>
  );
}
