import useOrderService from "../../services/OrderService";
import { useState, useEffect } from "react";

import { Button } from "@mui/material";
import "../style/style.scss";
import './registerForm.scss';

const RegisterForm = ({setToken}) => {

    const {registerUser, error, clearError} = useOrderService();

    const [firstName, setFirstName] = useState();
    const [lastName, setLastName] = useState();
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [itsOk, setItsOk] = useState(false);
    const [isRequest, setIsRequest] = useState(false);

    const handleSubmit = async e => {
        e.preventDefault();

        setIsRequest(true);

        const data = await registerUser({
            firstName,
            lastName,
            email,
            password,
            isCustomer: true
        });

        console.log(data);

        if (data?.status === 500){
			console.log('Очистка формы')
			e.target.reset(); 
		}
        else {
            setToken(data.token);
            setItsOk(true);
        }

        setIsRequest(false);
    }

    useEffect(() => {
        clearError();
    }, []);

    return (
        <div className="box">
            <form onSubmit={handleSubmit}> 
                <div className="text input">
                    <label>
                        <p>Имя</p>
                        <input type="text" onChange={e => setFirstName(e.target.value)}/>
                    </label>
                </div>
                <div className="text input">
                    <label>
                        <p>Фамилия</p>
                        <input type="text" onChange={e => setLastName(e.target.value)}/>
                    </label>
                </div>
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
                    <Button variant="contained" size="medium" type="submit">Далее</Button>
                </div>   
            </form>
        </div>
    )
}

export default RegisterForm;