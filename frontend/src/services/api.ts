import axios from "axios";
import Swal from "sweetalert2";

const instance = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  withCredentials: false,
});

function responseInterceptor(response) {
  if (response?.response?.status === 400 && response?.response?.data) {
    Swal.fire({
      title: "Sorry, an error has happened",
      icon: "error",
      html: response.response.data.message,
    });
  }
  return response;
}

instance.interceptors.response.use((response) => response, responseInterceptor);

export default instance;
