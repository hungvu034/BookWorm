pipeline{
    agent any
    stages{
        stage("build"){
            steps{
                echo "========building your app========"
            }
            post{
                always{
                    sh 'dotnet build '
                }
                success{
                    echo "========build executed successfully========"
                }
                failure{
                    echo "========build execution failed========"
                }
            }
        }
        stage("run"){
            steps{
                echo "=======running your app========="
            }
            post{
                always{
                    sh 'dotnet run'
                }
                success{
                    echo "========run executed successfully========"
                }
                failure{
                    echo "========run execution failed========"
                }
            }
        }
    }
    
    post{
        always{
            echo "========always========"
        }
        success{
            echo "========pipeline executed successfully ========"
        }
        failure{
            echo "========pipeline execution failed========"
        }
    }
}