import http from "k6/http";
import { check } from "k6";

export let options = {
  stages: [
    { duration: "30s", target: 50 }, // Ramp up to 10 users
    { duration: "1m", target: 50 }, // Stay at 10 users
    { duration: "30s", target: 0 }, // Ramp down to 0 users
  ],
};

export default function () {
  const url = "http://127.0.0.1:80/AccountService/GetAccounts"; // Replace with your API URL
  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };

  const res = http.get(url, params); // Use http.post(url, payload, params) for POST requests
  check(res, {
    "status is 200": (r) => r.status === 200,
    "response time < 500ms": (r) => r.timings.duration < 500,
  });
}
