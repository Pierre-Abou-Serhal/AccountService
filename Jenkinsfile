pipeline {
    agent any

    stages {
        stage('Clone') {
            steps {
                 git(branch: 'main', url: 'file:///var/jenkins_home/workspace/accountservice')
            }
        }
        stage('Build') {
            steps {
                echo 'Building...'
                // Add build steps here
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                // Add test steps here
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying...'
                // Add deploy steps here
            }
        }
    }
}