import axios from "axios";

const instance = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  withCredentials: false,
});

// function responseErrorInterceptor() {}

// instance.interceptors.response.use(
//   (response) => response,
//   responseErrorInterceptor
// );

export default instance;
