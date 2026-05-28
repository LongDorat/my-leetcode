#include "Solution.h"

#include <gtest/gtest.h>

TEST(SolutionTest, RunReturnsDefault) {
    Solution solution;
    int result = solution.Run();
    // Add assertions here
    EXPECT_EQ(result, 0);
}
