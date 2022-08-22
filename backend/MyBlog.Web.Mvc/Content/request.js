import axios from "axios";

const instance = axios.create({
  baseURL: "http://localhost:5001/",
  timeout: 2500,
});

export default instance;
