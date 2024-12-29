pipeline {
	agent any

    environment {
        DOCKER_IMAGE = "accountservice"
        REGISTRY = "localhost:5000" // Local Docker registry
    }

    stages {
        stage('Clone') {
            steps {
                git(branch: 'main', url: 'https://github.com/Pierre-Abou-Serhal/AccountService.git')
            }
        }

        stage('Restore & Build') {
            steps {
                echo 'Restoring and Building...'
                sh 'docker --version'
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}
