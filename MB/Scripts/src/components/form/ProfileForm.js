import React ,{PropTypes}from 'react';
import {reduxForm} from 'redux-form';
import {Form, Input, Select, Row, Col, Radio, Upload, message, Button, Icon } from 'antd';
const RadioGroup = Radio.Group;

import {createValidator, required } from '../../utils/validation';

const fields = ['sex', 'avatar', 'birthday'];
const validate = createValidator({
    sex: [required],
    avatar: [required],
    birthday: [required]
});

var ProfileForm = React.createClass({

    propTypes: {
        fields: PropTypes.object.isRequired,
        handleSubmit: PropTypes.func.isRequired,
        submitting: PropTypes.bool.isRequired,
        formError: PropTypes.array,
        initialValues: PropTypes.object
    },

    render(){

        const {fields: {sex, avatar,birthday}, handleSubmit, submitting} = this.props;

        return (
            <Form onSubmit={handleSubmit}>
                <Form.Item
                    label="gender:"
                    labelCol={{span: 3}}
                    wrapperCol={{span: 17}}>
                    <RadioGroup {...sex}>
                        <Radio value={'False'}>male</Radio>
                        <Radio value={'True'}>female</Radio>
                    </RadioGroup>
                </Form.Item>

                <div className='form-group'>
                    <label>Avatar</label>
                    <Upload {...avatar}>
                        <Button type="ghost">
                            <Icon type="upload"/>Upload Images
                        </Button>
                    </Upload>
                    {avatar.touched && avatar.error && <div>{avatar.error}</div>}
                </div>


                <div className='form-group'>
                    <button disabled={submitting} onClick={handleSubmit} type='submit' className='btn btn-default'>
                        {submitting ? <i/> : <i/>} Submit
                    </button>
                </div>

            </Form>
        );
    }
});

export default reduxForm({
        form: 'profileForm',
        fields,
        validate
    }
)(ProfileForm);

