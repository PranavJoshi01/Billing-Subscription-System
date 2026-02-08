import { useEffect, useState } from "react";
import api from "../services/api";

export default function Invoices() {
  const [invoices, setInvoices] = useState([]);

  useEffect(() => {
    api.get("/Invoices/my").then(res => setInvoices(res.data));
  }, []);

  return (
    <div className="container mt-4">
      <h3 className="mb-3">My Invoices</h3>

      {invoices.length === 0 ? (
        <p className="text-muted">No invoices generated yet.</p>
      ) : (
        <table className="table table-bordered">
          <thead className="table-light">
            <tr>
              <th>ID</th>
              <th>Amount</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {invoices.map(i => (
              <tr key={i.id}>
                <td>{i.id}</td>
                <td>₹{i.amount}</td>
                <td>{i.paymentStatus}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
