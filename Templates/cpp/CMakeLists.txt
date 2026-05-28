cmake_minimum_required(VERSION 3.15)
cmake_policy(SET CMP0135 NEW)

project(LeetCodeTemplate LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

include(FetchContent)

add_library(solution_lib Solution.cpp)

add_executable(UnitTest UnitTest.cpp)
target_link_libraries(UnitTest PRIVATE solution_lib gtest_main)

FetchContent_Declare(
	googletest
	URL https://github.com/google/googletest/archive/refs/tags/v1.14.0.zip
)
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)
FetchContent_MakeAvailable(googletest)

enable_testing()
include(GoogleTest)
gtest_discover_tests(UnitTest)