import { Button } from "@mui/material";
import {useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";

import "./loginForm.scss";
import "../style/style.scss";

import useOrderService from "../../services/OrderService";

const LoginForm = ({setToken, setIsAuth}) => {
    const {loginUser, error, clearError} = useOrderService();
    const navigate = useNavigate();

    const [email, setEmail] = useState();
    const [password, setPassword] = useState();


    const handleSubmit = async e => {
		e.preventDefault();
        
		const data = await loginUser({
			email: email,
		  	password: password
		});

        console.log(data);

		if (data?.status === 500){
			e.target.reset(); 
		}
		else{
			setToken(data.token);
            console.log(data.token);
			setIsAuth(true);
            console.log(data.typeUser);
            console.log(data.Id);
			if (data.typeUser === 'Customer'){
                sessionStorage.setItem('typeUser', 'Customer');
                navigate('/home'); 
            }
			else {
                sessionStorage.setItem('typeUser', 'Courier');
                navigate('/courier');
            }
		}
	}

    useEffect(() => {
        clearError();
    }, [])

    let errorMessage = (
        <div>
            <span style={{'color': '#ffffff', 'font-size': '1em'}}>
                Произошла ошибка
            </span>
        </div>
    )

    errorMessage = error ? errorMessage : null;

    return(
        <div className="box">
            <form onSubmit={handleSubmit}> 
                <div className="email input">
                    <label>
                        <p>Почта</p>
                        <input type="text" onChange={e => setEmail(e.target.value)}/>
                    </label>
                </div>
                <div className="password input">
                    <label>
                        <p>Пароль</p>
                        <input type="password" onChange={e => setPassword(e.target.value)}/>
                    </label>
                </div>
                
                <div className="button input">
                    <Button variant="contained" size="medium" type="submit">Войти!</Button>
                </div>     
                {errorMessage}
            </form>
        </div>
        
    )
}

export default LoginForm;