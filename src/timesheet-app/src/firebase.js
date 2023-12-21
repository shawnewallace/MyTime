// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
	apiKey: "AIzaSyBCj344y01yooWoWOIxRNseOFm1JS3Rd9g",
	authDomain: "mytime-24300.firebaseapp.com",
	projectId: "mytime-24300",
	storageBucket: "mytime-24300.appspot.com",
	messagingSenderId: "416410848775",
	appId: "1:416410848775:web:8e05d82fbfcbf2ef9d24a2"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
export const auth = getAuth(app);
export default app;