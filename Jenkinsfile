#!/usr/bin/env groovy

// 単体テスト実行ブランチ(develop/～のみ実施)
def TARGET_BRANCH = 'develop'

pipeline {

    //実行ノードを設定
    agent{
        label 'master'
    }

    //同一ジョブの並行実行不可(並行実行可能とするならoptionごと削除)
    options{
        disableConcurrentBuilds()
    }

    stages{
        //ワークスペースクリア
        stage ('Clean') {
            steps{
                script{
                    cleanWs()
                }
            }
        }

        //チェックアウト
        stage('Checkout'){
            steps{
                script{
                    //チェックアウト
                    checkout scm
                }
            }
        }

        //ビルド
        stage('Build'){
            steps{
                script{
                    echo "ビルド実行"
                    bat "\"C:\\Program Files\\MSBuild\\14.0\\Bin\\MSBuild.exe\" msbuild-sample.sln /t:Rebuild /p:Configuration=Release;Platform=\"Any CPU\" /l:FileLogger,Microsoft.Build.Engine;logfile=rebuild.log"
                }
            }
        }

        //単体テスト
        stage ('Test') {
            when{
                allOf{expression {return env.BRANCH_NAME.contains("${TARGET_BRANCH}")}}
            }
            steps {
                script {
                    try {
                        echo "テスト実行"
                        bat "\"C:\\Program Files\\Microsoft Visual Studio 14.0\\Common7\\IDE\\MSTest.exe\" /resultsfile:TestResults.xrt /noisolation /testcontainer:sample.Tests\\bin\\Release\\sample.Tests.dll"
                    } catch (Exception e) {
                        echo "単体テストエラー"
                    } finally {
                        echo "テスト結果集計"
                        xunit([MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: 'TestResults.xrt', skipNoTestFiles: false, stopProcessingIfError: true)])
                    }
                }
            }
        }
    }

    post{
        always{
            //成果物保存
            archiveArtifacts artifacts: '**/sample/bin/**/*', fingerprint: true
            archiveArtifacts artifacts: '**/rebuild.log'

            //ワークスペースクリア
            cleanWs()
        }
        failure{
            echo "エラー"
        }
        success{
            echo "正常"
        }
        unstable{
            echo "不安定"
        }
    }
}