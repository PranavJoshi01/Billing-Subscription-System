import { useEffect, useState } from "react";
import api from "../services/api";

export default function Plans() {
  const [plans, setPlans] = useState([]);

  useEffect(() => {
    api.get("/SubscriptionPlans").then(res => setPlans(res.data));
  }, []);

  const subscribe = async (id) => {
    await api.post(`/Subscriptions/${id}`);
    alert("Subscribed successfully");
  };

  return (
    <div className="container mt-4">
      <h3 className="mb-3">Available Subscription Plans</h3>

      <div className="row">
        {plans.map(p => (
          <div className="col-md-4" key={p.id}>
            <div className="card shadow-sm mb-4">
              <div className="card-body">
                <h5>{p.name}</h5>
                <p>₹{p.price} / {p.durationInDays} days</p>
                <button className="btn btn-success"
                  onClick={() => subscribe(p.id)}>
                  Subscribe
                </button>
              </div>
            </div>
          </div>
        ))}

        {plans.length === 0 && (
          <p className="text-muted">No plans available yet.</p>
        )}
      </div>
    </div>
  );
}
