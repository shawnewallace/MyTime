import { useNavigate } from "react-router-dom";
import { UserAuth } from "../../context/AuthContext";

const Profile = () => {
	const { user, logout } = UserAuth();
	const navigate = useNavigate();

	const handleLogout = async (e) => {
		try {
			await logout();
			navigate('/');
			console.log('You are logged out')
		} catch (e) {
			console.log(e.message);
		}
	}

	return (
		<div className="container">
			<div className="row justify-content-center">
				<div className="col-md-4 text-center">
					<p>Welcome <em className="text-decoration-underline">{user.email}</em>. You are logged in!</p>
					<div className="d-grid gap-2">
						<button type="submit" className="btn btn-primary pt-3 pb-3" onClick={(e) => handleLogout(e)}>Logout</button>
					</div>
				</div>
			</div>
		</div>
	)
}

export default Profile;